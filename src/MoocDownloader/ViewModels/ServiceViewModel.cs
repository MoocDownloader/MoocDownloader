using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Credentials;
using MoocDownloader.Views;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MoocDownloader.ViewModels;

public partial class ServiceViewModel : SharedDialogViewModel
{
    [ObservableProperty]
    private string _keyword = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Service> _services = new();

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    [NotifyCanExecuteChangedFor(nameof(VisitCommand))]
    private Service? _selectedService;

    /// <inheritdoc />
    public ServiceViewModel(IContainer container) : base(container)
    {
    }

    /// <inheritdoc />
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        LoadServices();
    }

    private void LoadServices(string? keyword = null)
    {
        if (Resources["ServiceList"] is not Service[] services)
        {
            throw new ArgumentNullException(nameof(services), "No service available.");
        }

        if (!string.IsNullOrEmpty(keyword))
        {
            services = services.Where(service => service.Url.Contains(keyword)).ToArray();
        }

        Services.Clear();
        Services.AddRange(services);
    }

    [RelayCommand]
    private void Select(Service? service)
    {
        SelectedService = service;
    }

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private void Login(Service? service)
    {
        if (service is null) return;

        Debug.WriteLine(service.Name);

        DialogService.ShowDialog(name: nameof(AuthenticationView));
    }

    private bool CanLogin(Service? service)
    {
        return service is not null;
    }

    [RelayCommand(CanExecute = nameof(CanVisit))]
    private void Visit(Service? service)
    {
        if (service is null) return;

        Process.Start(new ProcessStartInfo
        {
            FileName = service.Url,
            UseShellExecute = true,
        });
    }

    private bool CanVisit(Service? service)
    {
        return service is not null;
    }

    [RelayCommand]
    private void Search(string? keyword)
    {
        if (string.IsNullOrEmpty(keyword)) return;

        LoadServices(keyword);
    }

    [RelayCommand]
    private void Clear()
    {
        if (string.IsNullOrEmpty(Keyword)) return;

        Keyword = string.Empty;
        LoadServices();
    }
}