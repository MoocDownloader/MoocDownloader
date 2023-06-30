using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace MoocDownloader.Models.Credentials;

public partial class Service : ObservableObject
{
    [ObservableProperty]
    private ImageSource? _image;

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _url = string.Empty;

    [ObservableProperty]
    private bool _supportPassword;

    [ObservableProperty]
    private bool _supportBrowser;

    [ObservableProperty]
    private bool _supportCookie;
}