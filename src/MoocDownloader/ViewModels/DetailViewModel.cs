using CommunityToolkit.Mvvm.ComponentModel;
using DryIoc;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the detail view.
/// </summary>
public partial class DetailViewModel : SharedViewModel
{
    [ObservableProperty]
    private ObservableCollection<int> _units = new();

    /// <inheritdoc />
    public DetailViewModel(IContainer container) : base(container)
    {
        for (var i = 0; i < 10; i++)
        {
            Units.Add(i);
        }
    }
}