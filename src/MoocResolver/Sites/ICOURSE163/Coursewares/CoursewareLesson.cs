using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Coursewares;

public class CoursewareLesson
{
    [JsonPropertyName("id")]
    public long LessonId { get; set; }

    [JsonPropertyName("gmtCreate")]
    public long CreationTimestamp { get; set; }

    [JsonPropertyName("gmtModified")]
    public long ModifiedTimestamp { get; set; }

    [JsonPropertyName("name")]
    public string LessonName { get; set; } = string.Empty;

    [JsonPropertyName("termId")]
    public long TermId { get; set; }

    [JsonPropertyName("chapterId")]
    public long ChapterId { get; set; }

    [JsonPropertyName("contentType")]
    public CoursewareContentType ContentType { get; set; } = CoursewareContentType.Other;

    [JsonPropertyName("contentId")]
    public long? ContentId { get; set; }

    [JsonPropertyName("units")]
    public List<CoursewareUnit>? Units { get; set; }

    [JsonPropertyName("releaseTime")]
    public long ReleaseTimestamp { get; set; }
}