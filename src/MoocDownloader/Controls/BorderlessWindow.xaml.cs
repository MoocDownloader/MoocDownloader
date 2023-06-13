using Prism.Services.Dialogs;
using System.Windows;

namespace MoocDownloader.Controls;

/// <summary>
/// Interaction logic for BorderlessWindow.xaml
/// </summary>
public partial class BorderlessWindow : IDialogWindow
{
    public BorderlessWindow()
    {
        Owner = Application.Current.MainWindow;
        InitializeComponent();
    }

    /// <inheritdoc />
    public IDialogResult? Result { get; set; }
}