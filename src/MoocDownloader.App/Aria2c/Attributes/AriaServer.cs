using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoocDownloader.App.Aria2c.Attributes
{
    public class ServerDetail
    {
        [JsonProperty("currentUri")]
        public string CurrentUri { get; set; }

        [JsonProperty("downloadSpeed")]
        public string DownloadSpeed { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }

    public class AriaServer
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("servers")]
        public List<ServerDetail> Servers { get; set; }
    }
}
