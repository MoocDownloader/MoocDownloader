using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MoocDownloader.App.Aria2c.Attributes
{
    public class AriaFile
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("length")]
        public string Length { get; set; }

        [JsonProperty("completedLength")]
        public string CompletedLength { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("selected")]
        public string Selected { get; set; }

        [JsonProperty("uris")]
        public List<Uri> Uris { get; set; }
    }
}
