using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Coursewares;

public class CoursewareVideoSign
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("videoId")]
    public long VideoId { get; set; }

    [JsonPropertyName("duration")]
    public long Duration { get; set; }

    [JsonPropertyName("videoImgUrl")]
    public string? VideoImageurl { get; set; }

    [JsonPropertyName("signature")]
    public string? Signature { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}