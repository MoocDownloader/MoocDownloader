using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels;

public abstract partial class SharedViewModel : ObservableRecipient
{
    protected readonly IContainer Container;
    protected readonly IDialogService DialogService;

    protected SharedViewModel(IContainer container)
    {
        Container = container;
        DialogService = container.Resolve<IDialogService>();
    }

    [RelayCommand]
    protected virtual async Task Create()
    {
        DialogService.ShowDialog(
            name: nameof(CreationView),
            callback: result => { Trace.TraceInformation(result.Result.ToString()); });
        await Task.CompletedTask;
    }
}