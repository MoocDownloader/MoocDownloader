using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Creations;
using MoocDownloader.ViewModels.Shared;
using MoocResolver.Contracts;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Net;
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

    [RelayCommand]
    private async Task ResolveAsync()
    {
        if (string.IsNullOrEmpty(Url))
        {
            return;
        }

        var matchedCredential = MatchCredential(Url);

        // TODO: Check the matched credential.

        using var resolver = new ResolverBuilder().MatchLink(Url).Build(new ResolverOption
        {
            Url = Url,
            Credential = new ResolverCredential
            {
                Username = "", // TODO
                Password = "", // TODO
                Cookies = new CookieContainer(),
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
    /// <summary>
    /// Match credential by inputted URL.
    /// </summary>
    /// <param name="url">Inputted URL.</param>
    /// <returns>Matched credential.</returns>
    private WebsiteModel MatchCredential(string url)
    {
        return new WebsiteModel();
    }
}