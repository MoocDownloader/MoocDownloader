using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using MoocDownloader.Models.Playlists;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the detail view.
/// </summary>
public partial class DetailViewModel : SharedViewModel
{
    [ObservableProperty]
    private Playlist _playlist = new();

    /// <inheritdoc />
    public DetailViewModel(IContainer container) : base(container)
    {
    }
}