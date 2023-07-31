using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using MoocDownloader.Models.Downloads;
using MoocDownloader.ViewModels.Shared;

namespace MoocDownloader.ViewModels.Downloads;

/// <summary>
/// The view model of the detail view.
/// </summary>
public partial class DetailViewModel : SharedViewModel
{
    [ObservableProperty]
    private LibraryModel _library = new();

    /// <inheritdoc />
    public DetailViewModel(IContainer container) : base(container)
    {
    }
}