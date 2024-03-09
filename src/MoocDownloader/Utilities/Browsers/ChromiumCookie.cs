using SQLite;

namespace MoocDownloader.Utilities.Browsers;

[Table("cookies")]
public class ChromiumCookie
{
    [Column("host_key")]
    public string? Domain { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("value")]
    public string? Value { get; set; }

    [Column("encrypted_value")]
    public byte[]? EncryptedValue { get; set; }

    [Column("path")]
    public string? Path { get; set; }

    [Column("expires_utc")]
    public long Expires { get; set; }

    [Column("is_secure")]
    public bool IsSecure { get; set; }

    [Column("is_httponly")]
    public bool IsHttpOnly { get; set; }
}