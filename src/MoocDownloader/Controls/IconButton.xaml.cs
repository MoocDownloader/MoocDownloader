using MahApps.Metro.IconPacks;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for IconButton.xaml
/// </summary>
public partial class IconButton
{
    /// <summary>
    /// The icon of the button.
    /// </summary>
    public PackIconUniconsKind Icon
    {
        get => (PackIconUniconsKind)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register
    (
        "Icon", typeof(PackIconUniconsKind), typeof(IconButton), new PropertyMetadata(PackIconUniconsKind.None)
    );

    public IconButton()
    {
        InitializeComponent();
    }
}