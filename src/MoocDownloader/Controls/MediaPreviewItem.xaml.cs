using MoocDownloader.Models.Downloads;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for MediaPreviewItem.xaml
/// </summary>
public partial class MediaPreviewItem
{
    public MediaPreviewModel MediaPreview
    {
        get => (MediaPreviewModel)GetValue(MediaPreviewProperty);
        set => SetValue(MediaPreviewProperty, value);
    }

    public static readonly DependencyProperty MediaPreviewProperty =
        DependencyProperty.Register(nameof(MediaPreview), typeof(MediaPreviewModel), typeof(MediaPreviewItem));

    public MediaPreviewItem()
    {
        InitializeComponent();
    }
}