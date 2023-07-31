using MoocDownloader.Models.Downloads;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for TrashItem.xaml
/// </summary>
public partial class TrashItem
{
    public LibraryModel Playlist
    {
        get => (LibraryModel)GetValue(PlaylistProperty);
        set => SetValue(PlaylistProperty, value);
    }

    public static readonly DependencyProperty PlaylistProperty =
        DependencyProperty.Register(nameof(Playlist), typeof(LibraryModel), typeof(TrashItem));

    public TrashItem()
    {
        InitializeComponent();
    }
}