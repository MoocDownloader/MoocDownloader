using MoocDownloader.Models.Playlists;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for QueueItem.xaml
/// </summary>
public partial class QueueItem
{
    public Playlist Playlist
    {
        get => (Playlist)GetValue(PlaylistProperty);
        set => SetValue(PlaylistProperty, value);
    }

    public static readonly DependencyProperty PlaylistProperty =
        DependencyProperty.Register(nameof(Playlist), typeof(Playlist), typeof(QueueItem));

    public QueueItem()
    {
        InitializeComponent();
    }
}