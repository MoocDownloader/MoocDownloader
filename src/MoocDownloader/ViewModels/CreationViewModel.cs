using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoocDownloader.Models.Creations;
using MoocResolver.Contracts;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels;

public partial class CreationViewModel : ObservableRecipient, IDialogAware
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DownloadCommand))]
    private string _url = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DownloadCommand))]
    private string _path = string.Empty;

    [ObservableProperty]
    private ObservableCollection<MediaPreview> _mediaPreviews = new();

    [ObservableProperty]
    private bool _isResolving;

    public CreationViewModel()
    {
        Url = "https://www.icourse163.org/course/XMU-1001771003";

        for (var i = 0; i < 10; i++)
        {
            MediaPreviews.Add(new MediaPreview());
        }
    }

    [RelayCommand]
    private async Task ResolveAsync()
    {
        if (string.IsNullOrEmpty(Url))
        {
            return;
        }

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
        RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
    }

    private bool CanDownload()
    {
        return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Path);
    }

    [RelayCommand]
    private void Close()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
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

    /// <inheritdoc />
    public bool CanCloseDialog()
    {
        return true;
    }

    /// <inheritdoc />
    public void OnDialogClosed()
    {
    }

    /// <inheritdoc />
    public void OnDialogOpened(IDialogParameters parameters)
    {
    }

    /// <inheritdoc />
    public string Title { get; set; } = string.Empty;

    /// <inheritdoc />
    public event Action<IDialogResult>? RequestClose;
}