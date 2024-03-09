namespace MoocResolver.Contracts;

public enum AuthenticationType
{
    /// <summary>
    /// No authenticated.
    /// </summary>
    None,

    /// <summary>
    /// Authenticated by browser.
    /// </summary>
    Browser,

    /// <summary>
    /// Authenticated by cookies.
    /// </summary>
    Cookies,

    /// <summary>
    /// Authenticated by account.
    /// </summary>
    Account,

    /// <summary>
    /// Authenticated by QR code.
    /// </summary>
    QRCode,

    Other,
}