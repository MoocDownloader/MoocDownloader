using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Playlists;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the queue view.
/// </summary>
public partial class QueueViewModel : SharedViewModel
{
    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    public QueueViewModel(IContainer container) : base(container)
    {
    }

    [RelayCommand]
    private void Select(Playlist playlist)
    {
    }

    [RelayCommand]
    private void Toggle(Playlist playlist)
    {
    }
}