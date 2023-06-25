using System.Windows;
using static MoocDownloader.Helpers.WindowHelper;

namespace MoocDownloader.Views;

/// <summary>
/// Interaction logic for CredentialView.xaml
/// </summary>
public partial class CredentialView
{
    public CredentialView()
    {
        InitializeComponent();
        SetWindowCornerStyle(Window.GetWindow(this)!);
    }
}