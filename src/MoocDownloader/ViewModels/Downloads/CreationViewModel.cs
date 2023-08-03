using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Domain.Accounts;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Downloads;
using MoocDownloader.ViewModels.Shared;
using MoocResolver.Contracts;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels.Downloads;

public partial class CreationViewModel : SharedDialogViewModel
{
    private readonly AccountManager _accountManager;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DownloadCommand))]
    private string _url = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DownloadCommand))]
    private string _path = string.Empty;

    [ObservableProperty]
    private ObservableCollection<MediaPreviewModel> _mediaPreviews = new();

    [ObservableProperty]
    private bool _isResolving;

    /// <inheritdoc />
    public CreationViewModel(IContainer container) : base(container)
    {
        _accountManager = container.Resolve<AccountManager>();
    }

    /// <summary>
    /// Match website by inputted URL.
    /// </summary>
    /// <param name="url">Inputted URL.</param>
    /// <returns>Matched website.</returns>
    private WebsiteModel MatchWebsite(string url)
    {
        if (Resources["WebsiteList"] is not WebsiteModel[] websites)
        {
            throw new ArgumentNullException(nameof(websites), "No websites available.");
        }

        return websites.First(website => Regex.IsMatch(url, website.MatchPattern));
    }

    /// <summary>
    /// Execute logging to get Cookies.
    /// </summary>
    /// <param name="resolver"></param>
    /// <param name="resolverOption"></param>
    /// <param name="matchedWebsite"></param>
    /// <returns></returns>
    private async Task LoginAsync(
        IWebsiteResolver resolver,
        WebsiteResolverOption resolverOption,
        WebsiteModel matchedWebsite)
    {
        var cookies = await resolver.LoginAsync();

        // Update cookies.
        resolverOption.Account.Cookies = new CookieContainer();
        resolverOption.Account.Cookies.Add(cookies);

        // Save cookies.
        matchedWebsite.Account.CookieData = SerializeCookies(cookies);
        _accountManager.Update(matchedWebsite.Name, matchedWebsite.Account);
    }

    /// <summary>
    /// Parse cookie text into <see cref="CookieContainer"/>
    ///
    /// Text cookie text has two formats: JSON and Netscape.
    /// </summary>
    /// <param name="cookieData">The cookie text.</param>
    /// <returns><see cref="CookieContainer"/> parsed from cookie text.</returns>
    private static CookieContainer ParseCookies(string cookieData)
    {
        if (string.IsNullOrEmpty(cookieData))
        {
            throw new ArgumentNullException(nameof(cookieData));
        }

        // Detect the format of cookie text:
        //
        //     The cookie text is detected to be Netscape schema
        //     if each line of text except those starting with #
        //     matches Netscape pattern.
        //
        // File format:
        //
        // Officially, the first line of the file must be one of the following:
        // 
        //     # HTTP Cookie File
        //     # Netscape HTTP Cookie File 
        // 
        // Fields are separated by tab characters (\t or \009 or 0x09).
        // 
        //     Lines are separated by the newline format in use by the running operating system.
        //     That means CRLF (\r\n) for Windows and LF (\n) for Unix-like systems such as Linux, macOS, FreeBSD, etc. 
        //
        // The 7 fields are as follows:
        //
        //     +------------+----------------+---------------+-----------------------------------------------------+
        //     | Field Name | Type           | Example Value | Notes                                               |
        //     +---------------------------------------------------------------------------------------------------+
        //   0 | host       | string         | example.com   | Hostname that owns the cookie                       |
        //     +---------------------------------------------------------------------------------------------------+
        //   1 | subdomains | boolean string | FALSE         | Include subdomains (old attempt at SameSite)        |
        //     +---------------------------------------------------------------------------------------------------+
        //   2 | path       | string         | /             | Pathname that owns the cookie at the host           |
        //     +---------------------------------------------------------------------------------------------------+
        //   3 | isSecure   | boolean string | TRUE          | Send/receive cookie over HTTPS only.                |
        //     +---------------------------------------------------------------------------------------------------+
        //   4 | expiry     | number         | 1663611142    | Cookie expiration in standard Unix timestamp format |
        //     +---------------------------------------------------------------------------------------------------+
        //   5 | name       | string         | cookiename    | Cookie name                                         |
        //     +---------------------------------------------------------------------------------------------------+
        //   6 | value      | string         | cookievalue   | Cookie value                                        |
        //     +------------+----------------+---------------+-----------------------------------------------------+
        //
        // Source: http://fileformats.archiveteam.org/index.php?title=Netscape_cookies.txt

        const string netsacpePattern = @"^\S+\s{1}(TRUE|FALSE){1}\s{1}\S+\s{1}(TRUE|FALSE){1}\s{1}\d+\s{1}\S+\s{1}\S+";

        var cookieContainer = new CookieContainer();
        var cookieLines = cookieData.Split(Environment.NewLine).Where(line => !line.StartsWith("#")).ToList();

        if (cookieLines.All(line => Regex.IsMatch(line, netsacpePattern)))
        {
            // Netscape Format.
            foreach (var array in cookieLines.Select(cookieLine => cookieLine.Split('\t')))
            {
                if (array.Length != 7) throw new ArgumentException();

                try
                {
                    cookieContainer.Add(new Cookie(
                        name: array[5],
                        value: array[6],
                        path: array[2],
                        domain: array[0]));
                }
                catch (Exception)
                {
                    //
                }
            }
        }
        else
        {
            // JSON Format.
            var cookies = JsonSerializer.Deserialize<List<BrowserCookie>>(cookieData);

            if (cookies is null)
            {
                throw new ArgumentNullException(nameof(cookies));
            }

            foreach (var cookie in cookies)
            {
                try
                {
                    cookieContainer.Add(new Cookie(
                        name: cookie.Name ?? string.Empty,
                        value: cookie.Value,
                        path: cookie.Path,
                        domain: cookie.Domain));
                }
                catch (Exception)
                {
                    //
                }
            }
        }

        return cookieContainer;
    }

    /// <summary>
    /// Serialize CookieCollection to JSON.
    /// </summary>
    /// <param name="cookies">The cookies logged by username and password.</param>
    /// <returns>JSON format cookies text.</returns>
    private static string SerializeCookies(CookieCollection cookies)
    {
        var data = cookies.Select(cookie => new BrowserCookie
        {
            Domain = cookie.Domain,
            Name = cookie.Name,
            Value = cookie.Value,
            Path = cookie.Path,
            Expires = ((DateTimeOffset)cookie.Expires).ToUnixTimeSeconds(),
            IsSecure = cookie.Secure,
            IsHttpOnly = cookie.HttpOnly,
        }).ToList();
        return JsonSerializer.Serialize(data, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true,
        });
    }

    [RelayCommand]
    private async Task ResolveAsync()
    {
        if (string.IsNullOrEmpty(Url))
        {
            return;
        }

        var matchedWebsite = MatchWebsite(Url);
        var getResolverFunc = Container.Resolve<Func<WebsiteResolverOption, IWebsiteResolver>>(matchedWebsite.Resolver);

        //                              +-> Using Proxy
        //                              |
        //          +-> Network Proxy +-|-> PWD + Username
        //          |                   |
        //          |                   +-> Host + Port
        //          |
        //          |                   +-> Username + PWD
        // Option +-|-> Account       +-|
        //          |                   +-> Cookies
        //          |
        //          +-> URL
        var resolverOption = new WebsiteResolverOption
        {
            Url = Url,
            NetworkProxy = new NetworkProxy(),
            Account = new MoocResolver.Contracts.Account
            {
                Cookies = ParseCookies(matchedWebsite.Account.CookieData),
                Username = matchedWebsite.Account.Username,
                Password = matchedWebsite.Account.Password,
            }
        };
        var resolver = getResolverFunc(resolverOption);

        if (resolver.AuthenticationRequired)
        {
            switch (matchedWebsite.Account.Type)
            {
                case AccountType.None:
                    throw new ArgumentException(nameof(matchedWebsite.Account));
                case AccountType.Cookies:
                    if (!await resolver.CheckAsync())
                    {
                        throw new ArgumentException();
                    }

                    break;
                case AccountType.Password:
                    if (string.IsNullOrEmpty(matchedWebsite.Account.CookieData))
                    {
                        await LoginAsync(resolver, resolverOption, matchedWebsite);
                    }
                    else
                    {
                        if (!await resolver.CheckAsync())
                        {
                            await LoginAsync(resolver, resolverOption, matchedWebsite);
                        }
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        var library = await resolver.ResolveAsync();

        Debug.WriteLine(library.Name);
    }

    [RelayCommand(CanExecute = nameof(CanDownload))]
    private void Download()
    {
        var parameters = new DialogParameters();
        Close(new DialogResult(ButtonResult.OK, parameters));
    }

    private bool CanDownload()
    {
        return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Path);
    }

    [RelayCommand]
    private void Browse()
    {
        using var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog
        {
            Description = "选择保存文件夹",
            UseDescriptionForTitle = true,
            ShowNewFolderButton = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
        };
        var dialogResult = folderBrowserDialog.ShowDialog();

        if (dialogResult == System.Windows.Forms.DialogResult.OK)
        {
            Path = folderBrowserDialog.SelectedPath;
        }
    }
}