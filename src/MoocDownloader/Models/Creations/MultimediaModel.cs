using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace MoocDownloader.Models.Creations;

public partial class MultimediaModel : ObservableObject
{
    [ObservableProperty]
    private ImageSource? _image;

    [ObservableProperty]
    private string _fileName = string.Empty;

    [ObservableProperty]
    private long _fileSize;

    [ObservableProperty]
    private string? _mediaFormat;
}