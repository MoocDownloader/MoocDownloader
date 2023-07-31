using MoocDownloader.Domain.Contracts;
using MoocDownloader.Models.Accounts;
using SQLite;

namespace MoocDownloader.Domain.Accounts;

[Table("Accounts")]
public class Account : Entity
{
    [Indexed]
    [Column(nameof(WebsiteName))]
    public string WebsiteName { get; set; } = string.Empty;

    [Column(nameof(Type))]
    public AccountType Type { get; set; }

    [Column(nameof(Status))]
    public AccountStatus Status { get; set; }

    [Column(nameof(Username))]
    public string? Username { get; set; }

    [Column(nameof(Password))]
    public string? Password { get; set; }

    [Column(nameof(CookieData))]
    public string? CookieData { get; set; }
}