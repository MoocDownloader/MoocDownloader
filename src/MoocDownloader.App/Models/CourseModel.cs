using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoocDownloader.App.Models
{
    /// <summary>
    /// Course model.
    /// </summary>
    public class CourseModel
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("chapters")]
        public List<ChapterModel> Chapters { get; set; }

        [JsonProperty("courseName")]
        public string CourseName { get; set; }
    }
}