using Newtonsoft.Json;

namespace MoocResolver.Sites.ICOURSE163;

public class CourseTerm
{
    [JsonProperty("startTime")]
    public string? StartTimeTimestamp { get; set; }

    [JsonProperty("endTime")]
    public string? EndTimeTimestamp { get; set; }

    [JsonProperty("termId")]
    public string? TermId { get; set; }

    [JsonProperty("bigPhoto")]
    public string? PhotoUrl { get; set; }
}