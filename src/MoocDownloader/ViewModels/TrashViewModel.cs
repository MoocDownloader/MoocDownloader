using CommunityToolkit.Mvvm.ComponentModel;
using MoocDownloader.Models;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the trash view.
/// </summary>
[INotifyPropertyChanged]
public partial class TrashViewModel
{
    [ObservableProperty] private ObservableCollection<Course> _trashes = new();
}