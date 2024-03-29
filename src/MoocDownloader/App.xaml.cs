﻿using MoocDownloader.Controls;
using MoocDownloader.Services;
using MoocDownloader.Services.Contracts;
using MoocDownloader.ViewModels.Accounts;
using MoocDownloader.ViewModels.Dialogs;
using MoocDownloader.ViewModels.Downloads;
using MoocDownloader.ViewModels.Preferences;
using MoocDownloader.Views;
using MoocDownloader.Views.Accounts;
using MoocDownloader.Views.Dialogs;
using MoocDownloader.Views.Downloads;
using MoocDownloader.Views.Preferences;
using MoocResolver.Contracts;
using MoocResolver.Resolvers;
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
        containerRegistry.RegisterSingleton<ILanguageService, LanguageService>();
        containerRegistry.RegisterSingleton<IResourceService, ResourceService>();

        containerRegistry.Register<IResolver, BilibiliResolver>();
        containerRegistry.Register<IResolver, Course163Resolver>();
        containerRegistry.Register<IResolver, CoursesResolver>();
        containerRegistry.Register<IResolver, Study163Resolver>();
        containerRegistry.Register<IResolver, XuetangxResolver>();

        containerRegistry.RegisterDialogWindow<BorderlessWindow>();
        containerRegistry.RegisterDialog<CreationView, CreationViewModel>();
        containerRegistry.RegisterDialog<WebsiteListView, WebsiteListViewModel>();
        containerRegistry.RegisterDialog<AboutView, AboutViewModel>();
        containerRegistry.RegisterDialog<PreferenceView, PreferenceViewModel>();
        containerRegistry.RegisterDialog<AuthenticationView, AuthenticationViewModel>();
        containerRegistry.RegisterDialog<BrowserView, BrowserViewModel>();
        containerRegistry.RegisterDialog<MessageView, MessageViewModel>();
    }

    /// <inheritdoc />
    protected override Window CreateShell() => Container.Resolve<ShellView>();

    protected override void OnInitialized()
    {
        Akavache.Registrations.Start(applicationName: nameof(MoocDownloader));
        base.OnInitialized();
    }
}