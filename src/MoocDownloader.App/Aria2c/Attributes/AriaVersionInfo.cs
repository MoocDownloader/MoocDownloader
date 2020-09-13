using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoocDownloader.App.Aria2c.Attributes
{
    [JsonObject]
    public class AriaVersionInfo
    {
        [JsonProperty("enabledFeatures")]
        public List<string> EnabledFeatures { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}