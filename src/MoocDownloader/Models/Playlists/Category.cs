using CommunityToolkit.Mvvm.ComponentModel;

namespace MoocDownloader.Models.Playlists;

public partial class Category : ObservableObject
{
    [ObservableProperty]
    private int _index;

    [ObservableProperty]
    private string _name = string.Empty;
}