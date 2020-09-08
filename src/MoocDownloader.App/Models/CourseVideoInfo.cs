using System.Collections.Generic;

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
        public string VideoFileName { get; set; }

        /// <summary>
        /// list of downloaded ts files.
        /// </summary>
        public string MergeListFile { get; set; }

        /// <summary>
        /// ts files.
        /// </summary>
        public List<string> TSFiles { get; set; } = new List<string>();

        /// <summary>
        /// Whether or not to concat.
        /// </summary>
        public bool IsConcat { get; set; } = false;
    }
}