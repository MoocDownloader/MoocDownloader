using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DryIoc;
using MoocDownloader.Messages;
using MoocDownloader.Models.Downloads;
using MoocDownloader.Views.Downloads;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
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
            callback: result =>
            {
                if (result.Result != ButtonResult.OK) return;

                var library = result.Parameters.GetValue<LibraryModel>(nameof(LibraryModel));

                Libraries.Add(library);
            });
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void Select(LibraryModel? library)
    {
        SelectedLibrary = library;
        Messenger.Send(new LibrarySelectedMessage(SelectedLibrary));
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