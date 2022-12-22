namespace MoocDownloader.Models;

/// <summary>
/// The state of the course.
/// </summary>
public enum CourseState
{
    /// <summary>
    /// The course is paused.
    /// </summary>
    Paused,

    /// <summary>
    /// The course is downloading.
    /// </summary>
    Downloading,

    /// <summary>
    /// The course has been finished to download.
    /// </summary>
    Finished,

    /// <summary>
    /// The course has been deleted.
    /// </summary>
    Deleted,

    /// <summary>
    /// The course is missing.
    /// </summary>
    Missing,
}