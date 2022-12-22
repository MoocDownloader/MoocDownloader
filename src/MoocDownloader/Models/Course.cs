using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace MoocDownloader.Models;

/// <summary>
/// The model of the course.
/// </summary>
[INotifyPropertyChanged]
public partial class Course
{
    /// <summary>
    /// Name of the course.
    /// </summary>
    [ObservableProperty]
    private string _courseName = string.Empty;

    /// <summary>
    /// Download url of the course.
    /// </summary>
    [ObservableProperty]
    private string _downloadUrl = string.Empty;

    /// <summary>
    /// The cover of the course.
    /// </summary>
    [ObservableProperty]
    private string? _coverImage;

    /// <summary>
    /// Creation time of the course.
    /// </summary>
    [ObservableProperty]
    private DateTime? _creationTime;

    /// <summary>
    /// Finished time of the course.
    /// </summary>
    [ObservableProperty]
    private DateTime? _finishedTime;

    /// <summary>
    /// The state of the course.
    /// </summary>
    [ObservableProperty]
    private CourseState _courseState = CourseState.Paused;

    /// <summary>
    /// Course introduction.
    /// </summary>
    [ObservableProperty]
    private string _introduction = string.Empty;

    /// <summary>
    /// The count of course files that have been downloaded.
    /// </summary>
    [NotifyPropertyChangedFor(nameof(CurrentProgress))]
    [ObservableProperty]
    private int _completedCount;

    /// <summary>
    /// Total number of course files that will be downloaded.
    /// </summary>
    [NotifyPropertyChangedFor(nameof(CurrentProgress))]
    [ObservableProperty]
    private int _totalCount;

    /// <summary>
    /// Total size of course files.
    /// </summary>
    [ObservableProperty]
    private long _totalSize;

    /// <summary>
    /// Download progress of current course.
    /// </summary>
    public double CurrentProgress => (double)_completedCount / _totalCount * 100;
}