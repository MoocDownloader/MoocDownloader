using System.Windows;
using static MoocDownloader.Utilities.Windows.WindowUtility;

namespace MoocDownloader.Views.Downloads;

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