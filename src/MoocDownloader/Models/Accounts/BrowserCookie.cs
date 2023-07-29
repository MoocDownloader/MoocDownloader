using SQLite;
using System.Text.Json.Serialization;

namespace MoocDownloader.Models.Accounts;

[Table("cookies")]
public class BrowserCookie
{
    [JsonPropertyName("domain")]
    [Column("host_key")]
    public string? Host { get; set; }

    [JsonPropertyName("name")]
    [Column("name")]
    public string? Name { get; set; }

    [JsonPropertyName("value")]
    [Column("value")]
    public string? Value { get; set; }

    [JsonIgnore]
    [Column("encrypted_value")]
    public byte[]? EncryptedValue { get; set; }

    [JsonPropertyName("path")]
    [Column("path")]
    public string? Path { get; set; }

    [JsonPropertyName("expirationDate")]
    [Column("expires_utc")]
    public long Expires { get; set; }

    [JsonPropertyName("secure")]
    [Column("is_secure")]
    public bool IsSecure { get; set; }

    [JsonPropertyName("httpOnly")]
    [Column("is_httponly")]
    public bool IsHttpOnly { get; set; }
}