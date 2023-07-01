using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Playlists;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the ready view.
/// </summary>
public partial class ReadyViewModel : SharedPlaylistViewModel
{
    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    public ReadyViewModel(IContainer container) : base(container)
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