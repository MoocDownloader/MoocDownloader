using MoocDownloader.Models.Downloads;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for QueueItem.xaml
/// </summary>
public partial class QueueItem
{
    public LibraryModel Playlist
    {
        get => (LibraryModel)GetValue(PlaylistProperty);
        set => SetValue(PlaylistProperty, value);
    }

    public static readonly DependencyProperty PlaylistProperty =
        DependencyProperty.Register(nameof(Playlist), typeof(LibraryModel), typeof(QueueItem));

    public QueueItem()
    {
        InitializeComponent();
    }
}