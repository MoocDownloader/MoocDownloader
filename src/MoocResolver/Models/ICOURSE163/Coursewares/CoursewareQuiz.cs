using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareQuiz
{
    [JsonPropertyName("id")]
    public long QuizId { get; set; }

    [JsonPropertyName("gmtCreate")]
    public long CreationTimestamp { get; set; }

    [JsonPropertyName("gmtModified")]
    public long ModifiedTimestamp { get; set; }

    [JsonPropertyName("name")]
    public string QuizName { get; set; } = string.Empty;

    [JsonPropertyName("termId")]
    public long TermId { get; set; }

    [JsonPropertyName("chapterId")]
    public long ChapterId { get; set; }

    [JsonPropertyName("contentType")]
    public CoursewareContentType ContentType { get; set; } = CoursewareContentType.Other;

    [JsonPropertyName("contentId")]
    public long? ContentId { get; set; }

    [JsonPropertyName("releaseTime")]
    public long ReleaseTimestamp { get; set; }

    [JsonPropertyName("test")]
    public CoursewareTest? Test { get; set; }
}