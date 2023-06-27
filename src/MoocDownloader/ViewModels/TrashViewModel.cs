using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using MoocDownloader.Models.Playlists;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the trash view.
/// </summary>
public partial class TrashViewModel : SharedViewModel
{
    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    public TrashViewModel(IContainer container) : base(container)
    {
        for (var i = 0; i < 10; i++)
        {
            Playlists.Add(new Playlist());
        }
    }
}