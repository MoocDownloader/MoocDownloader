using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels;

public abstract partial class SharedPlaylistViewModel : SharedViewModel
{
    /// <inheritdoc />
    protected SharedPlaylistViewModel(IContainer container) : base(container)
    {
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