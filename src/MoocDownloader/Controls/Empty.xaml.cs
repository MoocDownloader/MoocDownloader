using System.Windows;
using System.Windows.Media;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for Empty.xaml
/// </summary>
public partial class Empty
{
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public double ImageOpacity
    {
        get => (double)GetValue(ImageOpacityProperty);
        set => SetValue(ImageOpacityProperty, value);
    }

    public string Tip
    {
        get => (string)GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }

    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(Empty));

    public static readonly DependencyProperty ImageOpacityProperty =
        DependencyProperty.Register(nameof(ImageOpacity), typeof(double), typeof(Empty), new PropertyMetadata(0.25));

    public static readonly DependencyProperty TipProperty =
        DependencyProperty.Register(nameof(Tip), typeof(string), typeof(Empty), new PropertyMetadata(string.Empty));

    public Empty()
    {
        InitializeComponent();
    }
}