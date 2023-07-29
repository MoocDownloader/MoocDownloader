using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Playlists;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Downloads;

/// <summary>
/// The view model of the queue view.
/// </summary>
public partial class QueueViewModel : SharedPlaylistViewModel
{
    public QueueViewModel(IContainer container) : base(container)
    {
    }

    [RelayCommand]
    private void Toggle(Playlist playlist)
    {
    }
}