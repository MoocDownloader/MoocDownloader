using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MoocDownloader.Models.Playlists;

public partial class AuthorModel : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _homePage = string.Empty;

    [ObservableProperty]
    private ImageSource? _photo;
}