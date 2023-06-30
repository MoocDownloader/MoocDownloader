using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoocDownloader.Models.Credentials;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MoocDownloader.ViewModels;

public partial class CredentialViewModel : ObservableRecipient, IDialogAware
{
    private readonly IDialogService _dialogService;

    [ObservableProperty]
    private ObservableCollection<Credential> _credentials = new();

    public CredentialViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    [RelayCommand]
    private void Close()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
    }

    [RelayCommand]
    private void Select(Credential credential)
    {
    }

    [RelayCommand]
    private void Create()
    {
        _dialogService.ShowDialog(
            name: nameof(ServiceView),
            callback: result => { Trace.TraceInformation(result.Result.ToString()); });
    }

    [RelayCommand]
    private void Check()
    {
    }

    [RelayCommand]
    private void Delete()
    {
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