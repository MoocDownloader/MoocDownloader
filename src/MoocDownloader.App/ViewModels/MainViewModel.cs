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
    public ObservableCollection<CourseModel> Queues   { get; } = new();
    public ObservableCollection<CourseModel> Finishes { get; } = new();
    public ObservableCollection<CourseModel> Trashes  { get; } = new();

    public MainViewModel()
    {
        Init();
    }

    void Init()
    {
        Queues.AddRange(Enumerable.Repeat(new CourseModel(), 20));
        Finishes.AddRange(Enumerable.Repeat(new CourseModel(), 20));
        Trashes.AddRange(Enumerable.Repeat(new CourseModel(), 20));
    }
}