using MahApps.Metro.IconPacks;
using System;
using System.Windows;
using System.Windows.Input;
using static MahApps.Metro.IconPacks.PackIconUniconsKind;

namespace MoocDownloader.App.Views;

/// <summary>
/// The main view.
/// </summary>
public partial class MainView
{
    public MainView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Drag move the view.
    /// </summary>
    private void DragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
        else
        {
            DragMove();
        }
    }

    /// <summary>
    /// Change the state of the view.
    /// </summary>
    private void ChangeWindowState(object sender, RoutedEventArgs e)
    {
        if (sender is FrameworkElement { Tag: string action } && Enum.TryParse(action, out PackIconUniconsKind kind))
        {
            WindowState = kind switch
            {
                Minus    => WindowState.Minimized,
                Square   => WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal,
                Multiply => WindowState.Minimized,
                _        => WindowState
            };
        }
    }

    /// <inheritdoc />
    protected override void OnClosing(CancelEventArgs e)
    {
        base.OnClosing(e);

        e.Cancel = true;
        WindowState = WindowState.Minimized;
    }
}

