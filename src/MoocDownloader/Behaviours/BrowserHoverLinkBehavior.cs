using CefSharp;
using CefSharp.Wpf;
using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace MoocDownloader.Behaviours;

public class BrowserHoverLinkBehavior : Behavior<ChromiumWebBrowser>
{
    public string HoverLink
    {
        get => (string)GetValue(HoverLinkProperty);
        set => SetValue(HoverLinkProperty, value);
    }

    public static readonly DependencyProperty HoverLinkProperty = DependencyProperty.Register(
        nameof(HoverLink),
        typeof(string),
        typeof(BrowserHoverLinkBehavior),
        new PropertyMetadata(string.Empty));

    /// <inheritdoc />
    protected override void OnAttached()
    {
        AssociatedObject.StatusMessage += OnStatusMessage;
    }

    /// <inheritdoc />
    protected override void OnDetaching()
    {
        AssociatedObject.StatusMessage -= OnStatusMessage;
    }

    private void OnStatusMessage(object? sender, StatusMessageEventArgs e)
    {
        if (sender is ChromiumWebBrowser browser)
        {
            browser.Dispatcher.Invoke(() => HoverLink = e.Value);
        }
    }
}