using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace MoocDownloader.Models.Creations;

public partial class DownloadLink : ObservableObject
{
    [ObservableProperty]
    private int _index;

    [ObservableProperty]
    private string _path = string.Empty;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _url = string.Empty;

    [ObservableProperty]
    private long _size;

    [ObservableProperty]
    private string _type = string.Empty;

    [ObservableProperty]
    private DateTime _creationTime = DateTime.Now;
}