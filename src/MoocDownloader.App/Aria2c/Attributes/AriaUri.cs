using Newtonsoft.Json;

namespace MoocDownloader.App.Aria2c.Attributes
{
    public class AriaUri
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
