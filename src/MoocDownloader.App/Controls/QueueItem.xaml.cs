using System.Windows;
using System.Windows.Media;

namespace MoocDownloader.App.Controls;

/// <summary>
/// Interaction logic for QueueItem.xaml
/// </summary>
public partial class QueueItem
{
    /// <summary>
    /// Name of the current course.
    /// </summary>
    public string CourseName
    {
        get => (string)GetValue(CourseNameProperty);
        set => SetValue(CourseNameProperty, value);
    }

    /// <summary>
    /// Number of files downloaded.
    /// </summary>
    public int DownloadedFiles
    {
        get => (int)GetValue(DownloadedFilesProperty);
        set => SetValue(DownloadedFilesProperty, value);
    }

    /// <summary>
    /// The total number of files that will be downloaded.
    /// </summary>
    public int TotalFiles
    {
        get => (int)GetValue(TotalFilesProperty);
        set => SetValue(TotalFilesProperty, value);
    }

    /// <summary>
    /// Download status of the current course.
    /// </summary>
    public string Status
    {
        get => (string)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <summary>
    /// Cover image of the current course.
    /// </summary>
    public ImageSource CoverImage
    {
        get => (ImageSource)GetValue(CoverImageProperty);
        set => SetValue(CoverImageProperty, value);
    }

    /// <summary>
    /// The progress of the current course.
    /// </summary>
    public int ProgressRate
    {
        get => (int)GetValue(ProgressRateProperty);
        set => SetValue(ProgressRateProperty, value);
    }

    #region Dependency Properties

    public static readonly DependencyProperty CourseNameProperty = DependencyProperty.Register
    (
        "CourseName", typeof(string), typeof(QueueItem), new PropertyMetadata(string.Empty)
    );

    public static readonly DependencyProperty DownloadedFilesProperty = DependencyProperty.Register
    (
        "DownloadedFiles", typeof(int), typeof(QueueItem), new PropertyMetadata(0)
    );

    public static readonly DependencyProperty TotalFilesProperty = DependencyProperty.Register
    (
        "TotalFiles", typeof(int), typeof(QueueItem), new PropertyMetadata(0)
    );

    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register
    (
        "Status", typeof(string), typeof(QueueItem), new PropertyMetadata(string.Empty)
    );

    public static readonly DependencyProperty CoverImageProperty = DependencyProperty.Register
    (
        "CoverImage", typeof(ImageSource), typeof(QueueItem)
    );

    public static readonly DependencyProperty ProgressRateProperty = DependencyProperty.Register
    (
        "ProgressRate", typeof(int), typeof(QueueItem), new PropertyMetadata(0)
    );

    #endregion

    public QueueItem()
    {
        InitializeComponent();
    }
}