using MoocDownloader.Models.Accounts;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for WebsiteItem.xaml
/// </summary>
public partial class WebsiteItem
{
    public WebsiteModel Website
    {
        get => (WebsiteModel)GetValue(WebsiteProperty);
        set => SetValue(WebsiteProperty, value);
    }

    public static readonly DependencyProperty WebsiteProperty =
        DependencyProperty.Register(nameof(Website), typeof(WebsiteModel), typeof(WebsiteItem));

    public WebsiteItem()
    {
        InitializeComponent();
    }
}