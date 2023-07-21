using SQLite;

namespace MoocDownloader.Models.Credentials;

[Table("cookies")]
public class BrowserCookie
{
    [Column("creation_utc")]
    public int Creation { get; set; }

    [Column("host_key")]
    public string? Host { get; set; }

    [Column("name")]
    public string? Name { get; set; }

    [Column("value")]
    public string? Value { get; set; }

    [Column("encrypted_value")]
    public byte[]? EncryptedValue { get; set; }

    [Column("path")]
    public string? Path { get; set; }

    [Column("expires_utc")]
    public int Expires { get; set; }

    [Column("is_secure")]
    public int IsSecure { get; set; }

    [Column("is_httponly")]
    public int IsHttpOnly { get; set; }

    [Column("has_expires")]
    public int HasExpires { get; set; }
}