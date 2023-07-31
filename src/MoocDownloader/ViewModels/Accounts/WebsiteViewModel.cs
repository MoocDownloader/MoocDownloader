using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Domain.Accounts;
using MoocDownloader.Models.Accounts;
using MoocDownloader.ViewModels.Shared;
using MoocDownloader.Views.Accounts;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MoocDownloader.ViewModels.Accounts;

public partial class WebsiteViewModel : SharedDialogViewModel
{
    private readonly AccountManager _accountManager;

    [ObservableProperty]
    private ObservableCollection<WebsiteModel> _websites = new();

    [ObservableProperty]
    private WebsiteModel? _selectedWebsite;

    [ObservableProperty]
    private string _keyword = string.Empty;

    /// <inheritdoc />
    public WebsiteViewModel(IContainer container) : base(container)
    {
        _accountManager = container.Resolve<AccountManager>();
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        LoadWebsites();
    }

    private void LoadWebsites()
    {
        if (Resources["WebsiteList"] is not WebsiteModel[] websites)
        {
            throw new ArgumentNullException(nameof(websites), "No websites available.");
        }

        if (!string.IsNullOrEmpty(Keyword))
        {
            websites = websites.Where(credential => credential.Url.Contains(Keyword)).ToArray();
        }

        Websites.Clear();
        Websites.AddRange(websites);
    }

    [RelayCommand]
    private void Select(WebsiteModel? website)
    {
        SelectedWebsite = website;
    }

    [RelayCommand]
    private void Clear()
    {
        Keyword = string.Empty;
        LoadWebsites();
    }

    [RelayCommand]
    private void Login(WebsiteModel? website)
    {
        if (website is null) return;

        var dialogParameters = new DialogParameters
        {
            { nameof(WebsiteModel), website }
        };

        DialogService.ShowDialog(
            name: nameof(AccountView),
            parameters: dialogParameters,
            callback: result =>
            {
                if (result is not { Result: ButtonResult.OK }) return;

                var account = _accountManager.GetAccountByWebSiteName(website.Name);

                if (account is null)
                {
                    _accountManager.Insert(website.Name, website.Account);
                }
                else
                {
                    _accountManager.Update(website.Name, website.Account);
                }
            });
    }

    [RelayCommand]
    private void Check(WebsiteModel? website)
    {
    }

    [RelayCommand]
    private void Visit(WebsiteModel? website)
    {
        if (website is null) return;

        Process.Start(new ProcessStartInfo
        {
            FileName = website.Url,
            UseShellExecute = true,
        });
    }
}