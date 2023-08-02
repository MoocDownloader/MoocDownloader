using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareResult<T>
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("result")]
    public T? Result { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("traceId")]
    public string? TraceId { get; set; }

    [JsonPropertyName("sampled")]
    public bool Sampled { get; set; }
}