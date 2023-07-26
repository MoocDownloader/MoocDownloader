using CefSharp.Wpf;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Credentials;
using Prism.Services.Dialogs;
using System;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels;

public partial class BrowserViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private Credential? _credential;

    [ObservableProperty]
    private IWpfWebBrowser? _browser;

    [ObservableProperty]
    private string _inputAddress = string.Empty;

    [ObservableProperty]
    private string? _currentAddress;

    /// <inheritdoc />
    public BrowserViewModel(IContainer container) : base(container)
    {
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        Credential = parameters.GetValue<Credential>(nameof(Credential));
        InputAddress = Credential.LoginUrl;
    }

    /// <inheritdoc />
    public override void OnDialogClosed()
    {
        try
        {
            Browser?.GetBrowser().CloseBrowser(true);
            Browser?.Dispose();
        }
        catch (Exception)
        {
            //
        }
    }

    /// <inheritdoc />
    protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.PropertyName)
        {
            // The URL will be loaded by the browser only when the instance of
            // Chromium browser is bound to the `Browser` property.
            case nameof(Browser):
                Browser?.LoadUrl(Credential?.LoginUrl ?? string.Empty);
                break;
        }
    }

    [RelayCommand]
    private async Task HomeAsync()
    {
        if (Credential is null || Browser is null) return;

        await Browser.LoadUrlAsync(Credential.Url);
    }

    [RelayCommand]
    private async Task GoAsync()
    {
        if (Credential is null || Browser is null) return;

        await Browser.LoadUrlAsync(InputAddress);
    }

    [RelayCommand]
    private async Task ConfirmAsync()
    {
        if (Credential is null || Browser is null) return;

        await Task.CompletedTask;
    }
}