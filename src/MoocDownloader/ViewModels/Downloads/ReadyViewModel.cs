using DryIoc;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Downloads;

/// <summary>
/// The view model of the ready view.
/// </summary>
public partial class ReadyViewModel : SharedPlaylistViewModel
{
    public ReadyViewModel(IContainer container) : base(container)
    {
    }
}