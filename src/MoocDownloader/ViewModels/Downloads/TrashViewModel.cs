using DryIoc;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Downloads;

/// <summary>
/// The view model of the trash view.
/// </summary>
public partial class TrashViewModel : SharedPlaylistViewModel
{
    public TrashViewModel(IContainer container) : base(container)
    {
    }
}