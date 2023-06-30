using MoocDownloader.Models.Credentials;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for ServiceItem.xaml
/// </summary>
public partial class ServiceItem
{
    public Service Service
    {
        get => (Service)GetValue(ServiceProperty);
        set => SetValue(ServiceProperty, value);
    }

    public static readonly DependencyProperty ServiceProperty =
        DependencyProperty.Register(nameof(Service), typeof(Service), typeof(ServiceItem));

    public ServiceItem()
    {
        InitializeComponent();
    }
}