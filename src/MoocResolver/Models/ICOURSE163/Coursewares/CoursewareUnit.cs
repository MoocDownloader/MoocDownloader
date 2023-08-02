using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareUnit
{
    [JsonPropertyName("id")]
    public long UnitId { get; set; }

    [JsonPropertyName("lessonId")]
    public long LessonId { get; set; }

    [JsonPropertyName("chapterId")]
    public long ChapterId { get; set; }

    [JsonPropertyName("termId")]
    public long TermId { get; set; }

    [JsonPropertyName("gmtCreate")]
    public long CreationTimestamp { get; set; }

    [JsonPropertyName("gmtModified")]
    public long ModifiedTimestamp { get; set; }

    [JsonPropertyName("name")]
    public string UnitName { get; set; } = string.Empty;

    [JsonPropertyName("contentType")]
    public CoursewareContentType ContentType { get; set; } = CoursewareContentType.Other;

    [JsonPropertyName("contentId")]
    public long? ContentId { get; set; }
}