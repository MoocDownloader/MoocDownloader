using MahApps.Metro.IconPacks;
using System.Windows;

namespace MoocDownloader.App.Controls;

/// <summary>
/// Interaction logic for IconTextTabHeader.xaml
/// </summary>
public partial class IconTextTabHeader
{
    /// <summary>
    /// Text for the header of the tab.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Icon for the header of the tab.
    /// </summary>
    public PackIconUniconsKind Icon
    {
        get => (PackIconUniconsKind)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register
    (
        "Text", typeof(string), typeof(IconTextTabHeader), new PropertyMetadata(string.Empty)
    );

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register
    (
        "Icon", typeof(PackIconUniconsKind), typeof(IconTextTabHeader), new PropertyMetadata(PackIconUniconsKind.None)
    );

    public IconTextTabHeader()
    {
        InitializeComponent();
    }
}