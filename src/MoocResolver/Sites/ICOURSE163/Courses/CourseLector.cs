using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Courses;

public class CourseLector
{
    [JsonPropertyName("lectorName")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("lectorTitle")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("lectorPhoto")]
    public string? PhotoUrl { get; set; }
}