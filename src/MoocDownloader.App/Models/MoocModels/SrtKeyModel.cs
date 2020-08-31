using Newtonsoft.Json;

namespace MoocDownloader.App.Models.MoocModels
{
    public class SrtKeyModel
    {
        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("nosKey")]
        public string NosKey { get; set; }
    }
}