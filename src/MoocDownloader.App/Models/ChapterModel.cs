using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoocDownloader.App.Models
{
    /// <summary>
    /// Chapter model.
    /// </summary>
    public class ChapterModel
    {
        [JsonProperty("contentId")]
        public long? ContentId { get; set; }

        [JsonProperty("contentType")]
        public int? ContentType { get; set; }

        [JsonProperty("draftStatus")]
        public int? DraftStatus { get; set; }

        [JsonProperty("gmtCreate")]
        public long? GmtCreate { get; set; }

        [JsonProperty("gmtModified")]
        public long? GmtModified { get; set; }

        [JsonProperty("hasFreePreviewVideo")]
        public bool? HasFreePreviewVideo { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("lessons")]
        public List<LessonModel> Lessons { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("releaseTime")]
        public long? ReleaseTime { get; set; }

        [JsonProperty("termId")]
        public long? TermId { get; set; }
    }
}