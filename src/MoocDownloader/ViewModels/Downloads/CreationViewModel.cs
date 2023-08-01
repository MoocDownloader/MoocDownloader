using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Downloads;
using MoocDownloader.ViewModels.Shared;
using MoocResolver.Contracts;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels.Downloads;

public partial class CreationViewModel : SharedDialogViewModel
{
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
    /// Parse cookie text into <see cref="CookieContainer"/>
    ///
    /// Text cookie text has two formats: JSON and Netscape.
    /// </summary>
    /// <param name="cookieData">The cookie text.</param>
    /// <returns><see cref="CookieContainer"/> parsed from cookie text.</returns>
    private CookieContainer ParseCookies(string cookieData)
    {
        if (string.IsNullOrEmpty(cookieData))
        {
            throw new ArgumentNullException(nameof(cookieData));
        }

        // Detect the format of cookie text:
        //
        //     The cookie text is detected to be Netscape schema
        //     if **EACH LINE** of text matches Netscape pattern.
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

        if (cookieData.Split(Environment.NewLine).All(line => Regex.IsMatch(line, netsacpePattern)))
        {
            // Netscape Format.
            var cookieLines = cookieData.Split(Environment.NewLine);

            foreach (var cookieLine in cookieLines)
            {
                var array = cookieLine.Split('\t');

                cookieContainer.Add(new Cookie(
                    name: array[5],
                    value: array[6],
                    path: array[2],
                    domain: array[0]));
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
                cookieContainer.Add(new Cookie(
                    name: cookie.Name ?? string.Empty,
                    value: cookie.Value,
                    path: cookie.Path,
                    domain: cookie.Host));
            }
        }

        return cookieContainer;
    }

    [RelayCommand]
    private async Task ResolveAsync()
    {
        if (string.IsNullOrEmpty(Url))
        {
            return;
        }

        var matchedWebsite = MatchWebsite(Url);
        var parsedCookie = ParseCookies(matchedWebsite.Account.CookieData);

        // TODO: Check the matched credential.

        using var resolver = new ResolverBuilder().MatchLink(Url).Build(new ResolverOption
        {
            Url = Url,
            Account = new Account
            {
                Username = matchedWebsite.Account.Username,
                Password = matchedWebsite.Account.Password,
                Cookies = parsedCookie,
            }
        });
        var _ = await resolver.ResolveAsync();
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