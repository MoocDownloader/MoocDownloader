using CefSharp;
using CefSharp.Wpf;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Dialogs.Messages;
using MoocDownloader.ViewModels.Shared;
using MoocDownloader.Views.Dialogs;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace MoocDownloader.ViewModels.Accounts;

public partial class BrowserViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private WebsiteModel? _website;

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
        CheckTimer.Elapsed += CheckTimerOnElapsed;
    }

    private void CheckTimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        var currentCookies = GetCurrentCookiesAsync().Result;

        if (!CheckCookies(currentCookies)) return;

        var browserCookies = ConvertCookies(currentCookies);
        var cookieDialogParameters = new DialogParameters
        {
            { nameof(BrowserCookie), browserCookies }
        };

        Application.Current.Dispatcher.Invoke(() =>
        {
            Close(new DialogResult(ButtonResult.OK, cookieDialogParameters));
        });
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        Website = parameters.GetValue<WebsiteModel>(nameof(WebsiteModel));
        InputAddress = Website.LoginUrl;

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
                Browser?.LoadUrl(Website?.LoginUrl ?? string.Empty);
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
        // The credential must include names for identifying cookie.
        if (Website is not { CookieNames.Count: > 0 }) return false;

        return Website.CookieNames.TrueForAll(
            name => cookies.Any(
                cookie => string.Equals(
                    cookie.Name, name, StringComparison.OrdinalIgnoreCase)));
    }

    private List<BrowserCookie> ConvertCookies(IEnumerable<Cookie> cookies)
    {
        return cookies.Select(cookie => new BrowserCookie
            {
                Host = cookie.Domain,
                Name = cookie.Name,
                Value = cookie.Value,
                Path = cookie.Path,
                Expires = ((DateTimeOffset)(cookie.Expires ?? DateTime.Now)).ToUnixTimeSeconds(),
                IsSecure = cookie.Secure,
                IsHttpOnly = cookie.HttpOnly,
            })
            .ToList();
    }

    [RelayCommand]
    private async Task HomeAsync()
    {
        if (Website is null || Browser is null) return;

        await Browser.LoadUrlAsync(Website.Url);
    }

    [RelayCommand]
    private async Task GoAsync()
    {
        if (Website is null || Browser is null) return;

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

        var browserCookies = ConvertCookies(currentCookies);
        var cookieDialogParameters = new DialogParameters
        {
            { nameof(BrowserCookie), browserCookies }
        };

        Close(new DialogResult(ButtonResult.OK, cookieDialogParameters));
    }
}