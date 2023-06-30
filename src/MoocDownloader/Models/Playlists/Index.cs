using CommunityToolkit.Mvvm.ComponentModel;

namespace MoocDownloader.Models.Playlists;

public partial class Index : ObservableObject
{
    [ObservableProperty]
    private bool _isGroup;

    [ObservableProperty]
    private string? _title;
}