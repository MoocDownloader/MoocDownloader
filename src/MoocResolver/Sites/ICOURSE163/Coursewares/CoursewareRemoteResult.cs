using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Coursewares;

public class CoursewareRemoteResult
{
    [JsonPropertyName("textPages")]
    public long? TextPages { get; set; }

    [JsonPropertyName("textOrigUrl")]
    public string? TextOriginalUrl { get; set; }

    [JsonPropertyName("textUrl")]
    public string? TextUrl { get; set; }
}