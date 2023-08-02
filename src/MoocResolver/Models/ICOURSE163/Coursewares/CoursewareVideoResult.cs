using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareVideoResult
{
    [JsonPropertyName("videoId")]
    public long VideoId { get; set; }

    [JsonPropertyName("duration")]
    public long Duration { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("videoImgUrl")]
    public string? VideoImageUrl { get; set; }

    [JsonPropertyName("srtCaptions")]
    public List<CoursewareCaption>? Captions { get; set; }

    [JsonPropertyName("videos")]
    public List<CoursewareVideo>? Videos { get; set; }
}