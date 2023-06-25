using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoocDownloader.Models;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

public partial class CredentialViewModel : ObservableRecipient, IDialogAware
{
    [ObservableProperty]
    private ObservableCollection<Credential> _credentials = new();

    public CredentialViewModel()
    {
        for (var i = 0; i < 10; i++)
        {
            Credentials.Add(new Credential());
        }
    }

    [RelayCommand]
    private void Close()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
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