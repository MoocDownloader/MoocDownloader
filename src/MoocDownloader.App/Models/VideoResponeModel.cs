using Newtonsoft.Json;

namespace MoocDownloader.App.Models
{
    /// <summary>
    /// Respone of /eds/api/v1/vod/video.
    /// </summary>
    public class VideoResponeModel
    {
        [JsonProperty("result")]
        public VideoResultModel Result { get; set; }
    }
}