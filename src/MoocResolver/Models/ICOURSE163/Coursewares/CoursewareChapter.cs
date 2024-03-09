using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareChapter
{
    [JsonPropertyName("id")]
    public long? ChapterId { get; set; }

    [JsonPropertyName("termId")]
    public long? TermId { get; set; }

    [JsonPropertyName("name")]
    public string ChapterName { get; set; } = string.Empty;

    [JsonPropertyName("gmtCreate")]
    public long? CreationTimestamp { get; set; }

    [JsonPropertyName("gmtModified")]
    public long? ModifiedTimestamp { get; set; }

    [JsonPropertyName("releaseTime")]
    public long? ReleaseTimestamp { get; set; }

    [JsonPropertyName("contentType")]
    public CoursewareContentType ContentType { get; set; } = CoursewareContentType.Other;

    [JsonPropertyName("contentId")]
    public long? ContentId { get; set; }

    [JsonPropertyName("lessons")]
    public List<CoursewareLesson>? Lessons { get; set; }

    [JsonPropertyName("homeworks")]
    public List<CoursewareHomework>? Homeworks { get; set; }

    [JsonPropertyName("quizs")]
    public List<CoursewareQuiz>? Quizzes { get; set; }

    [JsonPropertyName("exam")]
    public CoursewareExam? Exam { get; set; }
}