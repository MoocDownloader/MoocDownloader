using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Views;
using Prism.Services.Dialogs;

namespace MoocDownloader.ViewModels;

public partial class ShellViewModel : SharedViewModel
{
    /// <inheritdoc />
    public ShellViewModel(IContainer container) : base(container)
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
        DialogService.ShowDialog(name: nameof(AboutView), callback: _ => { });
    }

    [RelayCommand]
    private void ManageCredential()
    {
        DialogService.ShowDialog(name: nameof(CredentialView), callback: _ => { });
    }

    [RelayCommand]
    private void ChangePreference()
    {
        DialogService.ShowDialog(name: nameof(PreferenceView), callback: _ => { });
    }
}