using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using MoocDownloader.Services;
using Prism.Services.Dialogs;
using System.Windows;

namespace MoocDownloader.ViewModels;

public abstract class SharedViewModel : ObservableRecipient
{
    protected readonly IContainer Container;
    protected readonly IDialogService DialogService;
    protected readonly ResourceDictionary Resources;
    protected readonly LanguageService LanguageService;

    protected SharedViewModel(IContainer container)
    {
        Container = container;
        DialogService = container.Resolve<IDialogService>();
        Resources = container.Resolve<ResourceDictionary>();
        LanguageService = container.Resolve<LanguageService>();
    }
}