using MoocDownloader.App.Models;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;

namespace MoocDownloader.App.ViewModels;

/// <summary>
/// The view model of the main view.
/// </summary>
public class MainViewModel : BindableBase
{
    public ObservableCollection<CourseModel> Courses { get; set; } = new();

    public MainViewModel()
    {
        Init();
    }

    void Init()
    {
        Courses.AddRange(Enumerable.Repeat(new CourseModel(), 20));
    }
}