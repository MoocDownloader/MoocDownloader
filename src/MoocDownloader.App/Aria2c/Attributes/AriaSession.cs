using Newtonsoft.Json;

namespace MoocDownloader.App.Aria2c.Attributes
{
    [JsonObject]
    public class AriaSession
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}