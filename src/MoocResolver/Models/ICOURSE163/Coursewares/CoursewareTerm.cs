using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareTerm
{
    [JsonPropertyName("startTime")]
    public long StartTimestamp { get; set; }

    [JsonPropertyName("endTime")]
    public long EndTimestamp { get; set; }

    [JsonPropertyName("chapters")]
    public List<CoursewareChapter>? Chapters { get; set; } 

    [JsonPropertyName("exams")]
    public List<CoursewareExam>? Exams { get; set; }

    [JsonPropertyName("courseName")]
    public string? CourseName { get; set; }

    [JsonPropertyName("schoolId")]
    public long SchoolId { get; set; }
}