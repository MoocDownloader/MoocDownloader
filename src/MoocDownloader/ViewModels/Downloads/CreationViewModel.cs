using Akavache;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Dialogs.Messages;
using MoocDownloader.Models.Downloads;
using MoocDownloader.Utilities.Browsers;
using MoocDownloader.ViewModels.Shared;
using MoocDownloader.Views.Accounts;
using MoocDownloader.Views.Dialogs;
using MoocResolver.Contracts;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
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

    [ObservableProperty]
    private LibraryModel _library = new();

    /// <inheritdoc />
    public CreationViewModel(IContainer container) : base(container)
    {
    }

    /// <summary>
    /// Match website by inputted URL.
    /// </summary>
    /// <param name="url">Inputted URL.</param>
    /// <returns>Matched website.</returns>
    private WebsiteModel? MatchWebsite(string url)
    {
        if (Resources["WebsiteList"] is not WebsiteModel[] websites)
        {
            throw new ArgumentNullException(nameof(websites), "No websites available.");
        }

        return websites.FirstOrDefault(website => Regex.IsMatch(url, website.MatchPattern));
    }

    ///// <summary>
    ///// Execute logging to get Cookies.
    ///// </summary>
    ///// <param name="resolver"></param>
    ///// <param name="resolverOption"></param>
    ///// <param name="matchedWebsite"></param>
    ///// <returns></returns>
    //private async Task LoginAsync(
    //    IWebsiteResolver resolver,
    //    WebsiteResolverOption resolverOption,
    //    WebsiteModel matchedWebsite)
    //{
    //    var cookies = await resolver.LoginAsync();

    //    // Update cookies.
    //    resolverOption.Account.Cookies = new CookieContainer();
    //    resolverOption.Account.Cookies.Add(cookies);

    //    // Save cookies.
    //    //matchedWebsite.Authentication.CookieData = SerializeCookies(cookies);
    //    //_accountManager.Update(matchedWebsite.Name, matchedWebsite.Authentication);
    //}

    /// <summary>
    /// Serialize CookieCollection to JSON.
    /// </summary>
    /// <param name="cookies">The cookies logged by username and password.</param>
    /// <returns>JSON format cookies text.</returns>
    private static string SerializeCookies(CookieCollection cookies)
    {
        var data = cookies.Select(cookie => new ChromiumCookie()
        {
            Domain = cookie.Domain,
            Name = cookie.Name,
            Value = cookie.Value,
            Path = cookie.Path,
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

        if (matchedWebsite is null)
        {
            var messageOption = new MessageOption
            {
                Title = "提示",
                Message = "不支持输入的网址。",
                ConfirmText = "好的",
                MessageType = MessageType.Warning
            };
            var dialogParameters = new DialogParameters
            {
                { nameof(MessageOption), messageOption }
            };

            DialogService.ShowDialog(
                name: nameof(MessageView),
                parameters: dialogParameters,
                callback: _ => { });

            return;
        }

        // Get authentication from the local database.
        var authentication = await BlobCache.LocalMachine
            .GetObject<Authentication?>(matchedWebsite.Name)
            .Catch(Observable.Return<Authentication?>(null));

        // Open authentication view if there is no authentication in the local database.
        if (authentication is null)
        {
            var dialogParameters = new DialogParameters
            {
                { nameof(WebsiteModel), matchedWebsite }
            };
            var dialogResult = new DialogResult();

            DialogService.ShowDialog(
                name: nameof(AuthenticationView),
                parameters: dialogParameters,
                callback: result => dialogResult = (DialogResult)result);

            if (dialogResult is not { Result: ButtonResult.OK })
            {
                return;
            }

            authentication = dialogResult.Parameters.GetValue<Authentication>(nameof(Authentication));
        }

        var resolverFunc =
            Container.Resolve<Func<ResolverOption, MoocResolver.Contracts.IResolver>>(matchedWebsite.Resolver);
        var resolver = resolverFunc.Invoke(new ResolverOption
        {
            Url = Url,
            Authentication = authentication,
            NetworkProxy = new NetworkProxy()
        });
        var library = await resolver.ResolveAsync();

        foreach (var media in library.Medias)
        {
            MediaPreviews.Add(new MediaPreviewModel
            {
                FileName = media.FileName ?? string.Empty,
                FileSize = media.FileSize,
                MediaFormat = media.MediaFormat,
            });
        }

        Library.Name = library.Name;
        Library.Url = library.Url;
        Library.Introduction = library.Introduction;
        Library.Status = MediaStatus.Paused;
        Library.Categories.AddRange(library.Categories.Select(category => new CategoryModel
        {
            Index = category.Index,
            Name = category.Name,
        }));
        Library.Authors.AddRange(library.Authors.Select(author => new AuthorModel
        {
            Name = author.Name,
            Title = author.Title,
            HomePage = author.HomePage ?? string.Empty,
        }));
        Library.Medias.AddRange(library.Medias.Select(media => new MediaModel
        {
            Index = media.Index,
            FileName = media.FileName,
            FileUrl = media.FileUrl,
            FileSize = media.FileSize,
        }));
        foreach (var mediaGroup in library.Medias.GroupBy(media => media.GroupName))
        {
            Library.Indices.Add(new IndexModel
            {
                IsGroup = true,
                Title = mediaGroup.Key,
            });

            foreach (var media in mediaGroup)
            {
                Library.Indices.Add(new IndexModel
                {
                    Title = media.FileName,
                });
            }
        }
    }

    [RelayCommand(CanExecute = nameof(CanDownload))]
    private void Download()
    {
        Library.Path = Path;

        var parameters = new DialogParameters
        {
            { nameof(LibraryModel), Library }
        };

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