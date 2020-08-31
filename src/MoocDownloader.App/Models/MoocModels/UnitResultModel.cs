using Newtonsoft.Json;

namespace MoocDownloader.App.Models.MoocModels
{
    /// <summary>
    /// unit result model in lessons.
    /// </summary>
    public class UnitResultModel
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("learnedPosition")]
        public int? LearnedPosition { get; set; }

        [JsonProperty("textOrigUrl")]
        public string TextOrigUrl { get; set; }

        [JsonProperty("textPageWhRatio")]
        public double? TextPageWhRatio { get; set; }

        [JsonProperty("textPages")]
        public int? TextPages { get; set; }

        [JsonProperty("textUrl")]
        public string TextUrl { get; set; }

        [JsonProperty("videoLearnTime ")]
        public long? VideoLearnTime { get; set; }

        [JsonProperty("videoVo")]
        public VideoVoModel VideoVo { get; set; }
    }
}