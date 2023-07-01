using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Credentials;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MoocDownloader.ViewModels;

public partial class CredentialViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private ObservableCollection<Credential> _credentials = new();

    /// <inheritdoc />
    public CredentialViewModel(IContainer container) : base(container)
    {
    }

    [RelayCommand]
    private void Select(Credential credential)
    {
    }

    [RelayCommand]
    private void Create()
    {
        DialogService.ShowDialog(
            name: nameof(ServiceView),
            callback: result => { Trace.TraceInformation(result.Result.ToString()); });
    }

    [RelayCommand]
    private void Check()
    {
    }

    [RelayCommand]
    private void Delete()
    {
    }
}