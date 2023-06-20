using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Courses;

public class CourseSchool
{
    [JsonPropertyName("shortName")]
    public string? ShortName { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}