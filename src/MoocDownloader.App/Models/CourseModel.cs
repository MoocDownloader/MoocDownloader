using Prism.Mvvm;

namespace MoocDownloader.App.Models;

/// <summary>
/// The model of the course.
/// </summary>
public class CourseModel : BindableBase
{
    /// <summary>
    /// Name of the course.
    /// </summary>
    public string? CourseName { get; set; }

    /// <summary>
    /// Download url of the course.
    /// </summary>
    public string? DownloadUrl { get; set; }
}