using CommunityToolkit.Mvvm.ComponentModel;

namespace MoocDownloader.Models.Downloads;

public partial class MediaPreviewModel : ObservableObject
{
    [ObservableProperty]
    private string _fileName = string.Empty;

    [ObservableProperty]
    private long? _fileSize;

    [ObservableProperty]
    private string? _mediaFormat;
}