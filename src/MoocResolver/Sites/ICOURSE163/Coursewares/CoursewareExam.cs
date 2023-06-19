using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Coursewares;

public class CoursewareExam
{
    [JsonPropertyName("id")]
    public long ExamId { get; set; }

    [JsonPropertyName("name")]
    public string ExamName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("termId")]
    public long TermId { get; set; }

    [JsonPropertyName("deadline")]
    public long DeadlineTimestamp { get; set; }

    [JsonPropertyName("gmtCreate")]
    public long CreationTimestamp { get; set; }

    [JsonPropertyName("gmtModified")]
    public long ModifiedTimestamp { get; set; }

    [JsonPropertyName("chapterId")]
    public long ChapterId { get; set; }

    [JsonPropertyName("contentType")]
    public CoursewareContentType ContentType { get; set; } = CoursewareContentType.Other;
}