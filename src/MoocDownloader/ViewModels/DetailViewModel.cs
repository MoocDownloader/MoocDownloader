using System;
using CommunityToolkit.Mvvm.ComponentModel;
using MoocDownloader.Models;
using System.Collections.ObjectModel;

namespace MoocDownloader.ViewModels;

/// <summary>
/// The view model of the detail view.
/// </summary>
[INotifyPropertyChanged]
public partial class DetailViewModel
{
    [ObservableProperty]
    private ObservableCollection<int> _units = new();

    public DetailViewModel()
    {
        for (var i = 0; i < 10; i++)
        {
            Units.Add(i);
        }
    }
}