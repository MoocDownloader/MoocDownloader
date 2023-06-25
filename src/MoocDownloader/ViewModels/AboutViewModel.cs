using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Services.Dialogs;
using System;

namespace MoocDownloader.ViewModels;

public partial class AboutViewModel : ObservableRecipient, IDialogAware
{
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