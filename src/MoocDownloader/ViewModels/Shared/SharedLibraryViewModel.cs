using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DryIoc;
using MoocDownloader.Models.Downloads;
using MoocDownloader.Views.Downloads;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MoocDownloader.ViewModels.Shared;

public abstract partial class SharedLibraryViewModel : SharedViewModel
{
    [ObservableProperty]
    private ObservableCollection<LibraryModel> _libraries = new();

    [ObservableProperty]
    private LibraryModel? _selectedLibrary;

    /// <inheritdoc />
    protected SharedLibraryViewModel(IContainer container) : base(container)
    {
    }

    [RelayCommand]
    protected virtual async Task Create()
    {
        DialogService.ShowDialog(
            name: nameof(CreationView),
            callback: result => { Trace.TraceInformation(result.Result.ToString()); });
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void Select(LibraryModel library)
    {
        SelectedLibrary = library;
    }

    [RelayCommand]
    private void Open(LibraryModel library)
    {
    }

    [RelayCommand]
    private void CopyUrl(LibraryModel library)
    {
    }
}