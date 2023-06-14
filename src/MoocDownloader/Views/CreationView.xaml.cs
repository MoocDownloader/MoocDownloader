using System.Windows;
using static MoocDownloader.Helpers.WindowHelper;

namespace MoocDownloader.Views;

/// <summary>
/// Interaction logic for CreationView.xaml
/// </summary>
public partial class CreationView
{
    public CreationView()
    {
        InitializeComponent();
        SetWindowCornerStyle(Window.GetWindow(this)!);
    }
}