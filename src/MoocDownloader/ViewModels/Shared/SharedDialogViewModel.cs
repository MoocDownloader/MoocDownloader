using CommunityToolkit.Mvvm.Input;
using DryIoc;
using Prism.Services.Dialogs;
using System;

namespace MoocDownloader.ViewModels.Shared;

public abstract partial class SharedDialogViewModel : SharedViewModel, IDialogAware
{
    /// <inheritdoc />
    public event Action<IDialogResult>? RequestClose;

    /// <inheritdoc />
    public virtual string Title { get; set; } = string.Empty;

    /// <inheritdoc />
    protected SharedDialogViewModel(IContainer container) : base(container)
    {
    }

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
    }

    [RelayCommand]
    private void Close()
    {
        Close(new DialogResult(ButtonResult.Cancel));
    }

    protected void Close(IDialogResult dialogResult)
    {
        RequestClose?.Invoke(dialogResult);
    }
}