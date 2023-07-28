using CefSharp;
using CefSharp.Wpf;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Credentials;
using MoocDownloader.Models.Messages;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

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

    /// <summary>
    /// Timer for checking cookies.
    /// </summary>
    protected readonly Timer CheckTimer = new(TimeSpan.FromSeconds(2));

    /// <inheritdoc />
    public BrowserViewModel(IContainer container) : base(container)
    {
        // CheckTimer.Elapsed += CheckCookies;
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        Credential = parameters.GetValue<Credential>(nameof(Credential));
        InputAddress = Credential.LoginUrl;

        CheckTimer.Start();
    }

    /// <inheritdoc />
    public override void OnDialogClosed()
    {
        CheckTimer.Dispose();
        Browser?.Dispose();
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
            case nameof(CurrentAddress):
                InputAddress = CurrentAddress ?? string.Empty;
                break;
        }
    }

    private async Task<List<Cookie>> GetCurrentCookiesAsync()
    {
        var cookieManager = Cef.GetGlobalCookieManager();
        var cookieVisitor = new TaskCookieVisitor();

        cookieManager.VisitAllCookies(cookieVisitor);

        return await cookieVisitor.Task;
    }

    private bool CheckCookies(IReadOnlyCollection<Cookie> cookies)
    {
        if (Credential is null) return false;

        return Credential.CookieNames.TrueForAll(
            name => cookies.Any(
                cookie => string.Equals(
                    cookie.Name, name, StringComparison.OrdinalIgnoreCase)));
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
        var currentCookies = await GetCurrentCookiesAsync();

        if (!CheckCookies(currentCookies))
        {
            var messageOption = new MessageOption
            {
                Title = "Cookie 读取失败",
                Message = "未检测到有效的 Cookie 信息，确认是否已经正确登录网站。如果确认已经正确地登录，却仍然提示此信息，请向开发者报告此 Bug。",
                CancelText = string.Empty,
                ConfirmText = "确定",
                MessageType = MessageType.Error
            };
            var messageDialogParameters = new DialogParameters
            {
                { nameof(MessageOption), messageOption }
            };

            DialogService.ShowDialog(
                name: nameof(MessageView),
                parameters: messageDialogParameters,
                callback: _ => { });

            return;
        }

        var browserCookies = currentCookies.Select(cookie => new BrowserCookie
        {
            Host = cookie.Domain,
            Name = cookie.Name,
            Value = cookie.Value,
            Path = cookie.Path,
            Expires = ((DateTimeOffset)(cookie.Expires ?? DateTime.Now)).ToUnixTimeSeconds(),
            IsSecure = cookie.Secure,
            IsHttpOnly = cookie.HttpOnly,
        });
        var cookieDialogParameters = new DialogParameters
        {
            { nameof(BrowserCookie), browserCookies }
        };

        Close(new DialogResult(ButtonResult.OK, cookieDialogParameters));
    }
}