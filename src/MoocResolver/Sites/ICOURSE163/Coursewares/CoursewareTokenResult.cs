using System.Text.Json.Serialization;

namespace MoocResolver.Sites.ICOURSE163.Coursewares;

public class CoursewareTokenResult
{
    [JsonPropertyName("videoSignDto")]
    public CoursewareVideoSign? VideoSign { get; set; }

    [JsonPropertyName("learnVideoTime")]
    public long? LearnVideoTimestamp { get; set; }

    [JsonPropertyName("lessonUnitId")]
    public long LessonUnitId { get; set; }

    [JsonPropertyName("lessonUnitName")]
    public string? LessonUnitName { get; set; }

    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("contentType")]
    public CoursewareContentType ContentType { get; set; }
}