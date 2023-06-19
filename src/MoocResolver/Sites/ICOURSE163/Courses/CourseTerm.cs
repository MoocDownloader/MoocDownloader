using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Courses;

public class CourseTerm
{
    [JsonPropertyName("startTime")]
    public object? StartTimeTimestamp { get; set; }

    [JsonPropertyName("endTime")]
    public object? EndTimeTimestamp { get; set; }

    [JsonPropertyName("termId")]
    public string? TermId { get; set; }

    [JsonPropertyName("bigPhoto")]
    public string? PhotoUrl { get; set; }
}