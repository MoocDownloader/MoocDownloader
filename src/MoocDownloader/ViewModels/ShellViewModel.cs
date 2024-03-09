using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DryIoc;
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
    [ObservableProperty]
    private LibraryModel? _selectedLibrary;

    /// <inheritdoc />
    public ShellViewModel(IContainer container) : base(container)
    {
        IsActive = true;
    }

    /// <inheritdoc />
    public void Receive(LibrarySelectedMessage message)
    {
        SelectedLibrary = message.Value;
    }

    [RelayCommand]
    private void Initialize()
    {
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
        DialogService.ShowDialog(name: nameof(WebsiteListView), callback: _ => { });
    }

    [RelayCommand]
    private void ManagePreference()
    {
        DialogService.ShowDialog(name: nameof(PreferenceView), callback: _ => { });
    }
}