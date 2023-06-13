using System.Windows;
using System.Windows.Input;

namespace MoocDownloader.Assistants;

public class WindowAssistant
{
    public static readonly DependencyProperty CanDoubleMaximizeProperty = DependencyProperty.RegisterAttached(
        "CanDoubleMaximize",
        typeof(bool),
        typeof(WindowAssistant),
        new PropertyMetadata(false, OnCanDoubleMaximizeChanged));

    public static void SetCanDoubleMaximize(DependencyObject sender, bool value)
    {
        sender.SetValue(CanDoubleMaximizeProperty, value);
    }

    public static bool GetCanDoubleMaximize(DependencyObject sender)
    {
        return (bool)sender.GetValue(CanDoubleMaximizeProperty);
    }

    private static void OnCanDoubleMaximizeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is UIElement control && args.NewValue is bool value)
        {
            if (value)
            {
                control.MouseLeftButtonDown += DoubleMaximizeWindow;
            }
            else
            {
                control.MouseLeftButtonDown -= DoubleMaximizeWindow;
            }
        }
    }

    private static void DoubleMaximizeWindow(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2 &&
            sender is DependencyObject dependencyObject &&
            Window.GetWindow(dependencyObject) is { } window)
        {
            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
}