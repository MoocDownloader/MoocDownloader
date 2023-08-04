using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using DryIoc;
using MoocDownloader.Messages;
using MoocDownloader.Models.Downloads;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Downloads;

/// <summary>
/// The view model of the detail view.
/// </summary>
public partial class DetailViewModel : SharedViewModel, IRecipient<LibrarySelectedMessage>
{
    [ObservableProperty]
    private LibraryModel _library = new();

    /// <inheritdoc />
    public DetailViewModel(IContainer container) : base(container)
    {
        IsActive = true;
    }

    /// <inheritdoc />
    public void Receive(LibrarySelectedMessage message)
    {
        Library = message.Value ?? new LibraryModel();
    }
}