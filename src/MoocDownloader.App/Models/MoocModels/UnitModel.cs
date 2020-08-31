using Newtonsoft.Json;

namespace MoocDownloader.App.Models.MoocModels
{
    /// <summary>
    /// Unit model.
    /// </summary>
    public class UnitModel
    {
        [JsonProperty("chapterId")]
        public long? ChapterId { get; set; }

        [JsonProperty("contentId")]
        public long? ContentId { get; set; }

        [JsonProperty("contentType")]
        public int? ContentType { get; set; }

        [JsonProperty("durationInSeconds")]
        public int? DurationInSeconds { get; set; }

        [JsonProperty("gmtCreate")]
        public long? GmtCreate { get; set; }

        [JsonProperty("gmtModified")]
        public long? GmtModified { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("jsonContent")]
        public string JsonContent { get; set; }

        [JsonProperty("learnCount")]
        public string LearnCount { get; set; }

        [JsonProperty("lessonId")]
        public long? LessonId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("position")]
        public int? Position { get; set; }

        [JsonProperty("resourceInfo")]
        public string ResourceInfo { get; set; }

        [JsonProperty("termId")]
        public long? TermId { get; set; }

        [JsonProperty("unitId")]
        public long? UnitId { get; set; }
    }
}