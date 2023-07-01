using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Playlists;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the trash view.
/// </summary>
public partial class TrashViewModel : SharedPlaylistViewModel
{
    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    public TrashViewModel(IContainer container) : base(container)
    {
    }

    [RelayCommand]
    private void Select(Playlist playlist)
    {
    }

    [RelayCommand]
    private void Open(Playlist playlist)
    {
    }
}