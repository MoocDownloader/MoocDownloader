using CommunityToolkit.Mvvm.ComponentModel;

namespace MoocDownloader.Models.Downloads;

public partial class CategoryModel : ObservableObject
{
    [ObservableProperty]
    private int _index;

    [ObservableProperty]
    private string _name = string.Empty;
}