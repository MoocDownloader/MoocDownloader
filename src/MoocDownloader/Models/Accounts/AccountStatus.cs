namespace MoocDownloader.Models.Accounts;

public enum AccountStatus
{
    /// <summary>
    /// No account.
    /// </summary>
    None,

    /// <summary>
    /// Account is unverified.
    /// </summary>
    Unverified,

    /// <summary>
    /// Account is valid.
    /// </summary>
    Valid,

    /// <summary>
    /// Account is invalid.
    /// </summary>
    Invalid,
}