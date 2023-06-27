using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using System.Collections.ObjectModel;
using MoocDownloader.Models.Playlists;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the ready view.
/// </summary>
public partial class ReadyViewModel : SharedViewModel
{
    [ObservableProperty]
    private ObservableCollection<Playlist> _playlists = new();

    public ReadyViewModel(IContainer container) : base(container)
    {
        for (var i = 0; i < 10; i++)
        {
            Playlists.Add(new Playlist());
        }
    }
}