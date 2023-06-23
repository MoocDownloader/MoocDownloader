using System.Net;

namespace MoocResolver.Contracts;

public class ResolverOption
{
    public required string Link { get; set; }

    public ResolverCredential Credential { get; set; } = new();

    public ResolverNetworkProxy NetworkProxy { get; set; } = new();
}

public class ResolverCredential
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public CookieContainer Cookies { get; set; } = new();
}

public class ResolverNetworkProxy
{
    public bool? UseProxy { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public ushort Port { get; set; }
}