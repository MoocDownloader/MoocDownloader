using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Helpers;
using MoocDownloader.Models.Credentials;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MoocDownloader.ViewModels;

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
            callback: _ => { });
    }

    [RelayCommand]
    private void ImportCookies(string browser)
    {
        if (Credential is null) return;

        // Support to import cookies from Edge & Chrome browser.
        try
        {
            var domains = Credential.Domains.ToArray();
            var cookies = new List<BrowserCookie>();
            switch (browser)
            {
                case "Edge": // Import cookies from Edge browser.
                    cookies = BrowserHelper.ImportCookiesFromEdge(domains);
                    break;
                case "Chrome": // Import cookies from Chrome browser.
                    cookies = BrowserHelper.ImportCookiesFromChrome(domains);
                    break;
            }

            if (cookies.Any())
            {
                CookieData = JsonSerializer.Serialize(cookies, new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    WriteIndented = true,
                });
            }
        }
        catch (Exception)
        {
            // ignored
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