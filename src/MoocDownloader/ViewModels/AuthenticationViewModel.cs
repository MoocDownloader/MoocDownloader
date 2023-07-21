using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Helpers;
using MoocDownloader.Models.Credentials;
using Prism.Services.Dialogs;
using SQLite;
using System;
using System.IO;

namespace MoocDownloader.ViewModels;

public partial class AuthenticationViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private Credential? _credential;

    /// <inheritdoc />
    public AuthenticationViewModel(IContainer container) : base(container)
    {
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        Credential = parameters.GetValue<Credential>(nameof(Credential));
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

    [RelayCommand]
    private void SavePassword()
    {
        if (string.IsNullOrEmpty(Credential?.Username) || string.IsNullOrEmpty(Credential?.Password))
        {
            return;
        }

        ChangeCredentialStatus(CredentialStatus.Unverified);
        ChangeCredentialType(CredentialType.Password);

        Close(new DialogResult(result: ButtonResult.OK));
    }

    [RelayCommand]
    private void LaunchBrowser()
    {
    }

    [RelayCommand]
    private void ImportCookies(string browser)
    {
        if (Credential is null) return;

        // Support to import cookies from Edge & Chrome browser.
        try
        {
            var domains = Credential.Domains.ToArray();
            switch (browser)
            {
                case "Edge": // Import cookies from Edge browser.
                    var edgeCookies = BrowserHelper.ImportCookiesFromEdge(domains);
                    break;
                case "Chrome": // Import cookies from Chrome browser.
                    var chromeCookies = BrowserHelper.ImportCookiesFromChrome(domains);
                    break;
            }
        }
        catch (SQLiteException)
        {
        }
        catch (FileNotFoundException)
        {
        }
        catch (Exception)
        {
            // ignored
        }
    }

    [RelayCommand]
    private void SaveCookies()
    {
        if (string.IsNullOrEmpty(Credential?.CookieData))
        {
            return;
        }

        ChangeCredentialStatus(CredentialStatus.Unverified);
        ChangeCredentialType(CredentialType.Cookies);

        Close(new DialogResult(result: ButtonResult.OK));
    }
}