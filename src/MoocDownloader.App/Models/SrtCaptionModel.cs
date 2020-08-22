using Newtonsoft.Json;

namespace MoocDownloader.App.Models
{
    /// <summary>
    /// SRT Caption model.
    /// </summary>
    public class SrtCaptionModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("nosKey")]
        public string NosKey { get; set; }
    }
}