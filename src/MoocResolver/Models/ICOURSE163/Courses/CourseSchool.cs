using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Courses;

public class CourseSchool
{
    [JsonPropertyName("shortName")]
    public string? ShortName { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}