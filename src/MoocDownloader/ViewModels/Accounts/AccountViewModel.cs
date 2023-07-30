using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Helpers;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Dialogs.Messages;
using MoocDownloader.ViewModels.Shared;
using MoocDownloader.Views.Accounts;
using MoocDownloader.Views.Dialogs;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MoocDownloader.ViewModels.Accounts;

public partial class AccountViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private WebsiteModel? _website;

    [ObservableProperty]
    private string _cookieData = string.Empty;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    /// <inheritdoc />
    public AccountViewModel(IContainer container) : base(container)
    {
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        Website = parameters.GetValue<WebsiteModel>(nameof(WebsiteModel));

        switch (Website.Account.Type)
        {
            case AccountType.Cookies:
                CookieData = Website.Account.CookieData;
                break;
            case AccountType.Password:
                Username = Website.Account.Username;
                Password = Website.Account.Password;
                break;
        }
    }

    private void ChangeCredentialStatus(AccountStatus status)
    {
        if (Website is null) return;
        Website.Account.Status = status;
    }

    private void ChangeCredentialType(AccountType type)
    {
        if (Website is null) return;
        Website.Account.Type = type;
    }

    private void SetCredentialUsername()
    {
        if (Website is null) return;

        Website.Account.Username = Username;
        Website.Account.Password = Password;
    }

    private void SetCredentialCookieData()
    {
        if (Website is null) return;

        Website.Account.CookieData = CookieData;
    }

    private string SerializeCookies(List<BrowserCookie> cookies)
    {
        return JsonSerializer.Serialize(cookies, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true,
        });
    }

    [RelayCommand]
    private void SavePassword()
    {
        if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
        {
            return;
        }

        SetCredentialUsername();
        ChangeCredentialStatus(AccountStatus.Unverified);
        ChangeCredentialType(AccountType.Password);

        Close(new DialogResult(result: ButtonResult.OK));
    }

    [RelayCommand]
    private void LaunchBrowser()
    {
        var dialogParameters = new DialogParameters
        {
            { nameof(WebsiteModel), Website }
        };

        DialogService.ShowDialog(
            name: nameof(BrowserView),
            parameters: dialogParameters,
            callback: result =>
            {
                if (result.Result != ButtonResult.OK) return;

                var browserCookies = result.Parameters.GetValue<List<BrowserCookie>>(nameof(BrowserCookie));

                CookieData = SerializeCookies(browserCookies);

                // Save cookies.
                SetCredentialCookieData();
                ChangeCredentialStatus(AccountStatus.Valid);
                ChangeCredentialType(AccountType.Cookies);

                Close(new DialogResult(result: ButtonResult.OK));
            });
    }

    [RelayCommand]
    private void ImportCookies(string browser)
    {
        if (Website is null) return;

        // Support to import cookies from Edge & Chrome browser.
        try
        {
            var cookieDomains = Website.CookieDomains.ToArray();
            var browserCookies = new List<BrowserCookie>();
            switch (browser)
            {
                case "Edge": // Import cookies from Edge browser.
                    browserCookies = BrowserHelper.ImportCookiesFromEdge(cookieDomains);
                    break;
                case "Chrome": // Import cookies from Chrome browser.
                    browserCookies = BrowserHelper.ImportCookiesFromChrome(cookieDomains);
                    break;
            }

            if (browserCookies.Any())
            {
                CookieData = SerializeCookies(browserCookies);
            }
        }
        catch (Exception)
        {
            var messageOption = new MessageOption
            {
                Title = "Cookie 读取失败",
                Message = $"无法从浏览器 {browser} 中读取 Cookie 信息。从浏览器中读取 Cookie 信息之前应关闭浏览器，或正确安装支持的浏览器。",
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
        }
    }

    [RelayCommand]
    private void SaveCookies()
    {
        if (string.IsNullOrEmpty(CookieData))
        {
            return;
        }

        SetCredentialCookieData();
        ChangeCredentialStatus(AccountStatus.Unverified);
        ChangeCredentialType(AccountType.Cookies);

        Close(new DialogResult(result: ButtonResult.OK));
    }
}