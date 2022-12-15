using MoocDownloader.Views;
using Prism.Ioc;
using System.Windows;

namespace MoocDownloader;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    /// <inheritdoc />
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
    }

    /// <inheritdoc />
    protected override Window CreateShell() => Container.Resolve<MainView>();
}