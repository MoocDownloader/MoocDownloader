using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using MoocDownloader.Models.Playlists;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Downloads;

/// <summary>
/// The view model of the detail view.
/// </summary>
public partial class DetailViewModel : SharedViewModel
{
    [ObservableProperty]
    private PlaylistModel _playlist = new();

    /// <inheritdoc />
    public DetailViewModel(IContainer container) : base(container)
    {
    }
}