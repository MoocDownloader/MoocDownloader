using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareTest
{
    [JsonPropertyName("id")]
    public long TestId { get; set; }

    [JsonPropertyName("name")]
    public string? TestName { get; set; }

    [JsonPropertyName("type")]
    public int TestType { get; set; }

    [JsonPropertyName("releaseTime")]
    public long ReleaseTimestamp { get; set; }

    [JsonPropertyName("deadline")]
    public long DeadlineTimestamp { get; set; }

    [JsonPropertyName("totalScore")]
    public float TotalScore { get; set; }
}