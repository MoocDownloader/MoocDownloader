namespace MoocDownloader.Models.Accounts;

public enum AccountType
{
    /// <summary>
    /// No authenticated.
    /// </summary>
    None,

    /// <summary>
    /// Authenticated by cookies.
    /// </summary>
    Cookies,

    /// <summary>
    /// Authenticated by password.
    /// </summary>
    Password,
}