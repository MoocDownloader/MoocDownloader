using Newtonsoft.Json;

namespace MoocDownloader.App.Models
{
    public class CaptionModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}