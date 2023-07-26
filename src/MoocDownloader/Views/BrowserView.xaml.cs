using System.Windows.Controls;
using System.Windows.Input;

namespace MoocDownloader.Views;

/// <summary>
/// Interaction logic for BrowserView.xaml
/// </summary>
public partial class BrowserView
{
    public BrowserView()
    {
        InitializeComponent();
    }

    private void OnAddressTextBoxGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (sender is TextBox addressTextBox)
        {
            addressTextBox.SelectAll();
        }
    }

    private void OnAddressTextBoxGotMouseCapture(object sender, MouseEventArgs e)
    {
        if (sender is TextBox addressTextBox)
        {
            addressTextBox.SelectAll();
        }
    }
}