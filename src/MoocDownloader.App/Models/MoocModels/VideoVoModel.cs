using Newtonsoft.Json;
using System.Collections.Generic;

namespace MoocDownloader.App.Models.MoocModels
{
    public class VideoVoModel
    {
        [JsonProperty("duration")]
        public long? Duration { get; set; }

        [JsonProperty("encrypt")]
        public bool? Encrypt { get; set; }

        [JsonProperty("flvCaption")]
        public List<CaptionModel> FlvCaption { get; set; }

        [JsonProperty("isEncrypt")]
        public bool? IsEncrypt { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("mp4Caption")]
        public List<CaptionModel> Mp4Caption { get; set; }

        [JsonProperty("needKeyTimeValidate")]
        public bool? NeedKeyTimeValidate { get; set; }

        [JsonProperty("playerCollection")]
        public long? PlayerCollection { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("srtKeys")]
        public List<SrtKeyModel> SrtKeys { get; set; }

        [JsonProperty("start")]
        public long? Start { get; set; }

        [JsonProperty("videoId")]
        public long? VideoId { get; set; }
    }
}