using MoocDownloader.Models.Playlists;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for TrashItem.xaml
/// </summary>
public partial class TrashItem
{
    public Playlist Playlist
    {
        get => (Playlist)GetValue(PlaylistProperty);
        set => SetValue(PlaylistProperty, value);
    }

    public static readonly DependencyProperty PlaylistProperty =
        DependencyProperty.Register(nameof(Playlist), typeof(Playlist), typeof(TrashItem));

    public TrashItem()
    {
        InitializeComponent();
    }
}