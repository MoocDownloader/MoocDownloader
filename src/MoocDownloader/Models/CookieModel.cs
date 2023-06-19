using System.Text.Json.Serialization;

namespace MoocDownloader.Models;

public class CookieModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("domain")]
    public string? Domain { get; set; }

    [JsonPropertyName("hostOnly")]
    public bool HostOnly { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("secure")]
    public bool Secure { get; set; }

    [JsonPropertyName("httpOnly")]
    public bool HttpOnly { get; set; }

    [JsonPropertyName("sameSite")]
    public string? SameSite { get; set; }

    [JsonPropertyName("session")]
    public bool Session { get; set; }

    [JsonPropertyName("firstPartyDomain")]
    public string? FirstPartyDomain { get; set; }

    [JsonPropertyName("partitionKey")]
    public string? PartitionKey { get; set; }

    [JsonPropertyName("expirationDate")]
    public long? ExpirationDate { get; set; }

    [JsonPropertyName("storeId")]
    public string? StoreId { get; set; }
}