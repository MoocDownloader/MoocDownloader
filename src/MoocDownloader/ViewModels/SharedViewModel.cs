using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using Prism.Services.Dialogs;
using System.Windows;

namespace MoocDownloader.ViewModels;

public abstract class SharedViewModel : ObservableRecipient
{
    protected readonly IContainer Container;
    protected readonly IDialogService DialogService;
    protected readonly ResourceDictionary Resources;

    protected SharedViewModel(IContainer container)
    {
        Container = container;
        DialogService = container.Resolve<IDialogService>();
        Resources = container.Resolve<ResourceDictionary>();
    }
}