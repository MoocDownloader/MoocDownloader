using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace MoocDownloader.Models.Playlists;

public partial class PlaylistModel : ObservableObject
{
    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private string _url = string.Empty;

    [ObservableProperty]
    private string? _introduction;

    [ObservableProperty]
    private ImageSource? _coverImage;

    [ObservableProperty]
    private MediaStatus _status;

    [ObservableProperty]
    private string? _path;

    [ObservableProperty]
    private ObservableCollection<CategoryModel> _categories = new();

    [ObservableProperty]
    private ObservableCollection<AuthorModel> _authors = new();

    [ObservableProperty]
    private ObservableCollection<IndexModel> _indices = new();

    [NotifyPropertyChangedFor(nameof(TotalCount))]
    [NotifyPropertyChangedFor(nameof(TotalSize))]
    [NotifyPropertyChangedFor(nameof(Progress))]
    [ObservableProperty]
    private ObservableCollection<MediaModel> _medias = new();

    [ObservableProperty]
    private DateTime _creationTime = DateTime.Now;

    [ObservableProperty]
    private DateTime? _completionTime;

    [ObservableProperty]
    private TimeSpan _elapsedTime = TimeSpan.Zero;

    [NotifyPropertyChangedFor(nameof(Progress))]
    [ObservableProperty]
    private int _completedCount;

    public int TotalCount => Medias.Count;

    public long TotalSize => Medias.Sum(media => media.FileSize ?? 0);

    public double Progress => Medias.Count == 0 ? 0 : (double)CompletedCount / TotalCount * 100;
}