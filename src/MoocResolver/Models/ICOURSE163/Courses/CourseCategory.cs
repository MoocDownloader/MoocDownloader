using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Courses;

public class CourseCategory
{
    [JsonPropertyName("id")]
    public string Index { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}