using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Credentials;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MoocDownloader.ViewModels;

public partial class CredentialViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private ObservableCollection<Credential> _credentials = new();

    [ObservableProperty]
    private Credential? _selectedCredential;

    [ObservableProperty]
    private string _keyword = string.Empty;

    /// <inheritdoc />
    public CredentialViewModel(IContainer container) : base(container)
    {
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        LoadCredentials();
    }

    private void LoadCredentials()
    {
        if (Resources["CredentialList"] is not Credential[] credentials)
        {
            throw new ArgumentNullException(nameof(credentials), "No credentials available.");
        }

        if (!string.IsNullOrEmpty(Keyword))
        {
            credentials = credentials.Where(credential => credential.Url.Contains(Keyword)).ToArray();
        }

        Credentials.Clear();
        Credentials.AddRange(credentials);
    }

    [RelayCommand]
    private void Select(Credential? credential)
    {
        SelectedCredential = credential;
    }

    [RelayCommand]
    private void Clear()
    {
        Keyword = string.Empty;
        LoadCredentials();
    }

    [RelayCommand]
    private void Login(Credential? credential)
    {
        if (credential is null) return;

        var dialogParameters = new DialogParameters
        {
            { nameof(Credential), credential }
        };

        DialogService.ShowDialog(
            name: nameof(AuthenticationView),
            parameters: dialogParameters,
            callback: _ => { });
    }

    [RelayCommand]
    private void Check(Credential? credential)
    {
    }

    [RelayCommand]
    private void Visit(Credential? credential)
    {
        if (credential is null) return;

        Process.Start(new ProcessStartInfo
        {
            FileName = credential.Url,
            UseShellExecute = true,
        });
    }
}