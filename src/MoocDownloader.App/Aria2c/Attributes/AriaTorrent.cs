using Newtonsoft.Json;

namespace MoocDownloader.App.Aria2c.Attributes
{
    public class AriaTorrent
    {
        [JsonProperty("amChoking")]
        public string AmChoking { get; set; }

        [JsonProperty("bitfield")]
        public string BitField { get; set; }

        [JsonProperty("downloadSpeed")]
        public string DownloadSpeed { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("peerChoking")]
        public string PeerChoking { get; set; }

        [JsonProperty("peerId")]
        public string PeerId { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }

        [JsonProperty("seeder")]
        public string Seeder { get; set; }

        [JsonProperty("uploadSpeed")]
        public string UploadSpeed { get; set; }
    }
}
