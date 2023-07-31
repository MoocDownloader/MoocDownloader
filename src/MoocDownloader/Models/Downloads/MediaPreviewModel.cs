using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace MoocDownloader.Models.Downloads;

public partial class MediaPreviewModel : ObservableObject
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