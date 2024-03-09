using System.Text.Json.Serialization;

namespace MoocResolver.Models.ICOURSE163.Coursewares;

public class CoursewareTermResult
{
    [JsonPropertyName("lastLearnUnitId")]
    public long? LastLearnUnitId { get; set; }

    [JsonPropertyName("mocTermDto")]
    public CoursewareTerm? Term { get; set; }
}