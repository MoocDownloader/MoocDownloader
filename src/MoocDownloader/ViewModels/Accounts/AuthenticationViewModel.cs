using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Helpers;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Messages;
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

public partial class AuthenticationViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private Credential? _credential;

    [ObservableProperty]
    private string _cookieData = string.Empty;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    /// <inheritdoc />
    public AuthenticationViewModel(IContainer container) : base(container)
    {
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        Credential = parameters.GetValue<Credential>(nameof(Credential));

        switch (Credential.Type)
        {
            case CredentialType.Cookies:
                CookieData = Credential.CookieData;
                break;
            case CredentialType.Password:
                Username = Credential.Username;
                Password = Credential.Password;
                break;
        }
    }

    private void ChangeCredentialStatus(CredentialStatus status)
    {
        if (Credential is null) return;
        Credential.Status = status;
    }

    private void ChangeCredentialType(CredentialType type)
    {
        if (Credential is null) return;
        Credential.Type = type;
    }

    private void SetCredentialUsername()
    {
        if (Credential is null) return;

        Credential.Username = Username;
        Credential.Password = Password;
    }

    private void SetCredentialCookieData()
    {
        if (Credential is null) return;

        Credential.CookieData = CookieData;
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
        ChangeCredentialStatus(CredentialStatus.Unverified);
        ChangeCredentialType(CredentialType.Password);

        Close(new DialogResult(result: ButtonResult.OK));
    }

    [RelayCommand]
    private void LaunchBrowser()
    {
        var dialogParameters = new DialogParameters
        {
            { nameof(Credential), Credential }
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
                ChangeCredentialStatus(CredentialStatus.Valid);
                ChangeCredentialType(CredentialType.Cookies);

                Close(new DialogResult(result: ButtonResult.OK));
            });
    }

    [RelayCommand]
    private void ImportCookies(string browser)
    {
        if (Credential is null) return;

        // Support to import cookies from Edge & Chrome browser.
        try
        {
            var cookieDomains = Credential.CookieDomains.ToArray();
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
        ChangeCredentialStatus(CredentialStatus.Unverified);
        ChangeCredentialType(CredentialType.Cookies);

        Close(new DialogResult(result: ButtonResult.OK));
    }
}