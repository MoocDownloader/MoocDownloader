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
        containerRegistry.RegisterSingleton<ResourceDictionary>(() => Resources);
        containerRegistry.RegisterDialogWindow<BorderlessWindow>();
        containerRegistry.RegisterDialog<CreationView, CreationViewModel>();
        containerRegistry.RegisterDialog<CredentialView, CredentialViewModel>();
        containerRegistry.RegisterDialog<AboutView, AboutViewModel>();
        containerRegistry.RegisterDialog<PreferenceView, PreferenceViewModel>();
        containerRegistry.RegisterDialog<AuthenticationView, AuthenticationViewModel>();
        containerRegistry.RegisterDialog<BrowserView, BrowserViewModel>();
    }

    /// <inheritdoc />
    protected override Window CreateShell() => Container.Resolve<ShellView>();
}