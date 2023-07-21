namespace MoocDownloader.Models.Credentials;

public enum CredentialStatus
{
    /// <summary>
    /// No credential.
    /// </summary>
    None,

    /// <summary>
    /// Credential is unverified.
    /// </summary>
    Unverified,

    /// <summary>
    /// Credential is valid.
    /// </summary>
    Valid,

    /// <summary>
    /// Credential is invalid.
    /// </summary>
    Invalid,
}