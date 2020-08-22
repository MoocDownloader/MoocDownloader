using Newtonsoft.Json;

namespace MoocDownloader.App.Models
{
    /// <summary>
    /// Video model.
    /// </summary>
    public class VideoModel
    {
        [JsonProperty("quality")]
        public int? Quality { get; set; }

        [JsonProperty("size")]
        public long? Size { get; set; }

        [JsonProperty("videoUrl")]
        public string VideoUrl { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }
    }
}