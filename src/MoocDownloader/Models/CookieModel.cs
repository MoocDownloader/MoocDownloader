using Newtonsoft.Json;

namespace MoocDownloader.Models;

public class CookieModel
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("value")]
    public string? Value { get; set; }

    [JsonProperty("domain")]
    public string? Domain { get; set; }

    [JsonProperty("hostOnly")]
    public bool HostOnly { get; set; }

    [JsonProperty("path")]
    public string? Path { get; set; }

    [JsonProperty("secure")]
    public bool Secure { get; set; }

    [JsonProperty("httpOnly")]
    public bool HttpOnly { get; set; }

    [JsonProperty("sameSite")]
    public string? SameSite { get; set; }

    [JsonProperty("session")]
    public bool Session { get; set; }

    [JsonProperty("firstPartyDomain")]
    public string? FirstPartyDomain { get; set; }

    [JsonProperty("partitionKey")]
    public string? PartitionKey { get; set; }

    [JsonProperty("expirationDate")]
    public long? ExpirationDate { get; set; }

    [JsonProperty("storeId")]
    public string? StoreId { get; set; }
}