using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoocDownloader.Models.Messages;
using Prism.Services.Dialogs;
using System;

namespace MoocDownloader.ViewModels;

public partial class MessageViewModel : ObservableRecipient, IDialogAware
{
    [ObservableProperty]
    private MessageOption _messageOption = new();

    /// <inheritdoc />
    public event Action<IDialogResult>? RequestClose;

    /// <inheritdoc />
    public virtual string Title { get; set; } = string.Empty;

    /// <inheritdoc />
    public virtual bool CanCloseDialog()
    {
        return true;
    }

    /// <inheritdoc />
    public virtual void OnDialogClosed()
    {
    }

    /// <inheritdoc />
    public virtual void OnDialogOpened(IDialogParameters parameters)
    {
        MessageOption = parameters.GetValue<MessageOption>(nameof(MessageOption));
    }

    protected virtual void Close(IDialogResult dialogResult)
    {
        RequestClose?.Invoke(dialogResult);
    }

    [RelayCommand]
    private void Close()
    {
        Close(new DialogResult(ButtonResult.Cancel));
    }

    [RelayCommand]
    private void Confirm()
    {
        Close(new DialogResult(ButtonResult.OK));
    }
}