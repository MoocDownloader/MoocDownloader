using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoocDownloader.Models.Creations;
using MoocResolver.Sites.ICOURSE163;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MoocDownloader.Models;

namespace MoocDownloader.ViewModels;

public partial class CreationViewModel : ObservableRecipient, IDialogAware
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DownloadCommand))]
    private string _link = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(DownloadCommand))]
    private string _path = string.Empty;

    [ObservableProperty]
    private ObservableCollection<DownloadLink> _links = new();

    [ObservableProperty]
    private bool _isResolving;

    public CreationViewModel()
    {
        Link = "https://www.icourse163.org/course/XMU-1001771003";
    }

    [RelayCommand]
    private async Task ResolveAsync()
    {
        if (string.IsNullOrEmpty(Link))
        {
            return;
        }

        var cookieText = await File.ReadAllTextAsync(@"C:\Users\Kaede\Downloads\www.icourse163.org.json");
        var cookieData = JsonConvert.DeserializeObject<List<CookieModel>>(cookieText);
        var cookies = new CookieCollection();

        foreach (var model in cookieData!)
        {
            cookies.Add(new Cookie
            {
                Domain = model.Domain,
                HttpOnly = model.HttpOnly,
                Secure = model.Secure,
                Path = model.Path,
                Value = model.Value,
                Name = model.Name ?? string.Empty,
                Expires = DateTimeOffset.FromUnixTimeSeconds(model.ExpirationDate ?? 0).DateTime
            });
        }

        using var resolver = new Course163Resolver(Link, cookies);

        await resolver.ResolveAsync();
        await Task.CompletedTask;
    }

    [RelayCommand(CanExecute = nameof(CanDownload))]
    private void Download()
    {
        var parameters = new DialogParameters();
        RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameters));
    }

    private bool CanDownload()
    {
        return !string.IsNullOrEmpty(Link) && !string.IsNullOrEmpty(Path);
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

    /// <inheritdoc />
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == nameof(Link))
        {
        }
    }
}