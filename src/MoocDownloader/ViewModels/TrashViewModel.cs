using DryIoc;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the trash view.
/// </summary>
public partial class TrashViewModel : SharedPlaylistViewModel
{
    public TrashViewModel(IContainer container) : base(container)
    {
    }
}