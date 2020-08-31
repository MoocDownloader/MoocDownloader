namespace MoocDownloader.App.Models
{
    /// <summary>
    /// Information of the course video.
    /// </summary>
    public class CourseVideoInfo
    {
        /// <summary>
        /// Video will be saved path.
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// Video file name.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// m3u8 playlist download link.
        /// </summary>
        public string M3U8Link { get; set; }

        /// <summary>
        /// Whether or not to download.
        /// </summary>
        public bool IsDownload { get; set; } = false;
    }
}