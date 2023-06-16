using Newtonsoft.Json;

namespace MoocResolver.Sites.ICOURSE163;

public class CourseLector
{
    [JsonProperty("lectorName")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("lectorTitle")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("lectorPhoto")]
    public string? PhotoUrl { get; set; }
}