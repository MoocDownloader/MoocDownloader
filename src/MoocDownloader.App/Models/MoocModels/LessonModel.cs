using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoocDownloader.App.Models.MoocModels
{
    /// <summary>
    /// Lesson model.
    /// </summary>
    public class LessonModel
    {
        [JsonProperty("chapterId")]
        public long? ChapterId { get; set; }

        [JsonProperty("contentId")]
        public long? ContentId { get; set; }

        [JsonProperty("contentType")]
        public int? ContentType { get; set; }

        [JsonProperty("gmtCreate")]
        public long? GmtCreate { get; set; }

        [JsonProperty("gmtModified")]
        public long? GmtModified { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("isTestChecked")]
        public bool? IsTestChecked { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("releaseTime")]
        public long? ReleaseTime { get; set; }

        [JsonProperty("termId")]
        public long? TermId { get; set; }

        [JsonProperty("units")]
        public List<UnitModel> Units { get; set; }
    }
}