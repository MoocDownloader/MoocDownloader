using Newtonsoft.Json;

namespace MoocResolver.Sites.ICOURSE163;

public class CourseCategory
{
    [JsonProperty("id")]
    public string Index { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}