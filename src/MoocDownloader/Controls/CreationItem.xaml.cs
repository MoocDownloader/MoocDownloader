using MoocDownloader.Models.Creations;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for CreationItem.xaml
/// </summary>
public partial class CreationItem
{
    public MediaPreview MediaPreview
    {
        get => (MediaPreview)GetValue(MediaPreviewProperty);
        set => SetValue(MediaPreviewProperty, value);
    }

    public static readonly DependencyProperty MediaPreviewProperty =
        DependencyProperty.Register(nameof(MediaPreview), typeof(MediaPreview), typeof(CreationItem));

    public CreationItem()
    {
        InitializeComponent();
    }
}