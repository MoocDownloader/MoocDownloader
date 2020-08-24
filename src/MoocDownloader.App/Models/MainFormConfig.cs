namespace MoocDownloader.App.Models
{
    /// <summary>
    /// configuration of main form.
    /// </summary>
    public class MainFormConfig
    {
        /// <summary>
        /// course url.
        /// </summary>
        public string CourseUrl { get; set; } = string.Empty;

        /// <summary>
        /// course files saved path.
        /// </summary>
        public string CourseSavePath { get; set; } = string.Empty;

        /// <summary>
        /// whether to download the video.
        /// </summary>
        public bool IsDownloadVideo { get; set; } = true;

        /// <summary>
        /// whether to download the document.
        /// </summary>
        public bool IsDownloadDocument { get; set; } = true;

        /// <summary>
        /// whether to download the subtitle.
        /// </summary>
        public bool IsDownloadSubtitle { get; set; } = true;

        /// <summary>
        /// whether to download the attachment.
        /// </summary>
        public bool IsDownloadAttachment { get; set; } = true;

        /// <summary>
        /// video quality.
        /// </summary>
        public VideoQuality VideoQuality { get; set; } = VideoQuality.HD;
    }
}