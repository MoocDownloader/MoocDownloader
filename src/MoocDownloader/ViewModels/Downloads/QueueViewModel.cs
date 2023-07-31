using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Downloads;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Downloads;

/// <summary>
/// The view model of the queue view.
/// </summary>
public partial class QueueViewModel : SharedLibraryViewModel
{
    public QueueViewModel(IContainer container) : base(container)
    {
    }

    [RelayCommand]
    private void Toggle(LibraryModel library)
    {
    }
}