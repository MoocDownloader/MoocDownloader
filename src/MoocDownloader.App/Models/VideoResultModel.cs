using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoocDownloader.App.Models
{
    /// <summary>
    /// Video result model.
    /// </summary>
    public class VideoResultModel
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("defaultQuality")]
        public string DefaultQuality { get; set; }

        [JsonProperty("srtCaptions")]
        public List<SrtCaptionModel> SrtCaptions { get; set; }

        [JsonProperty("videos")]
        public List<VideoModel> Videos { get; set; }
    }
}