using MahApps.Metro.IconPacks;
using System.Windows;

namespace MoocDownloader.App.Controls;

/// <summary>
/// Interaction logic for IconTextButton.xaml
/// </summary>
public partial class IconTextButton
{
    /// <summary>
    /// The text of the button.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// The icon of the button.
    /// </summary>
    public PackIconUniconsKind Icon
    {
        get => (PackIconUniconsKind)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register
    (
        "Text", typeof(string), typeof(IconTextButton), new PropertyMetadata(string.Empty)
    );

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register
    (
        "Icon", typeof(PackIconUniconsKind), typeof(IconTextButton), new PropertyMetadata(PackIconUniconsKind.None)
    );

    public IconTextButton()
    {
        InitializeComponent();
    }
}