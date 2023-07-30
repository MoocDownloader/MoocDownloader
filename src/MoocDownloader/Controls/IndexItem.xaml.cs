using MoocDownloader.Models.Playlists;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for IndexItem.xaml
/// </summary>
public partial class IndexItem
{
    public IndexModel Index
    {
        get => (IndexModel)GetValue(IndexProperty);
        set => SetValue(IndexProperty, value);
    }

    public static readonly DependencyProperty IndexProperty =
        DependencyProperty.Register(nameof(Index), typeof(IndexModel), typeof(IndexItem));

    public IndexItem()
    {
        InitializeComponent();
    }
}