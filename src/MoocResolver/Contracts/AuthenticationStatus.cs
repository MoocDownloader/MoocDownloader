namespace MoocResolver.Contracts;

public enum AuthenticationStatus
{
    /// <summary>
    /// Not need authentication.
    /// </summary>
    None,

    /// <summary>
    /// Authentication is unverified.
    /// </summary>
    Unverified,

    /// <summary>
    /// Authentication is valid.
    /// </summary>
    Valid,

    /// <summary>
    /// Authentication is invalid.
    /// </summary>
    Invalid,
}