using CommunityToolkit.Mvvm.ComponentModel;
using MoocDownloader.Models;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the ready view.
/// </summary>
[INotifyPropertyChanged]
public partial class ReadyViewModel
{
    [ObservableProperty] private ObservableCollection<Course> _readies = new();
}