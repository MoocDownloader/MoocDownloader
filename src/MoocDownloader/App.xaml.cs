using MoocDownloader.Controls;
using MoocDownloader.ViewModels;
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
        containerRegistry.RegisterDialogWindow<BorderlessWindow>();
        containerRegistry.RegisterDialog<CreationView, CreationViewModel>();
    }

    /// <inheritdoc />
    protected override Window CreateShell() => Container.Resolve<ShellView>();
}