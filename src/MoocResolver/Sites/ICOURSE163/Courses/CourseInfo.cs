using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Courses;

public class CourseInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}