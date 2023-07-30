using MoocDownloader.Models.Creations;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for CreationItem.xaml
/// </summary>
public partial class CreationItem
{
    public MediaPreviewModel MediaPreview
    {
        get => (MediaPreviewModel)GetValue(MediaPreviewProperty);
        set => SetValue(MediaPreviewProperty, value);
    }

    public static readonly DependencyProperty MediaPreviewProperty =
        DependencyProperty.Register(nameof(MediaPreview), typeof(MediaPreviewModel), typeof(CreationItem));

    public CreationItem()
    {
        InitializeComponent();
    }
}