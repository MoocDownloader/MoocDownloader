using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DryIoc;
using MoocDownloader.Domain.Accounts;
using MoocDownloader.Messages;
using MoocDownloader.Models.Accounts;
using MoocDownloader.Models.Downloads;
using MoocDownloader.ViewModels.Shared;
using MoocDownloader.Views.Accounts;
using MoocDownloader.Views.Preferences;
using Prism.Services.Dialogs;
using System.Linq;

namespace MoocDownloader.ViewModels;

public partial class ShellViewModel : SharedViewModel, IRecipient<LibrarySelectedMessage>
{
    private readonly AccountManager _accountManager;

    [ObservableProperty]
    private LibraryModel? _selectedLibrary;

    /// <inheritdoc />
    public ShellViewModel(IContainer container) : base(container)
    {
        _accountManager = container.Resolve<AccountManager>();
        IsActive = true;
    }

    /// <inheritdoc />
    public void Receive(LibrarySelectedMessage message)
    {
        SelectedLibrary = message.Value;
    }

    private void LoadAccounts()
    {
        var accounts = _accountManager.GetList();

        if (Resources["WebsiteList"] is not WebsiteModel[] websites) return;

        foreach (var account in accounts)
        {
            var website = websites.First(website => website.Name == account.WebsiteName);

            website.Account.Username = account.Username ?? string.Empty;
            website.Account.Password = account.Password ?? string.Empty;
            website.Account.CookieData = account.CookieData ?? string.Empty;
            website.Account.Type = account.Type;
            website.Account.Status = account.Status;
        }
    }

    [RelayCommand]
    private void Initialize()
    {
        LoadAccounts();
    }

    [RelayCommand]
    private void VisitGitHub()
    {
    }

    [RelayCommand]
    private void NewIssue()
    {
    }

    [RelayCommand]
    private void AboutMe()
    {
        DialogService.ShowDialog(name: nameof(AboutView));
    }

    [RelayCommand]
    private void ManageAccount()
    {
        DialogService.ShowDialog(name: nameof(WebsiteView), callback: _ => { });
    }

    [RelayCommand]
    private void ManagePreference()
    {
        DialogService.ShowDialog(name: nameof(PreferenceView), callback: _ => { });
    }
}