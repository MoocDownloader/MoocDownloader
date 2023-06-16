using Newtonsoft.Json;

namespace MoocResolver.Sites.ICOURSE163;

public class CourseInfo
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}