using System.Net;

namespace MoocResolver.Contracts;

public class ResolverOption
{
    public required string Url { get; set; }

    public Account Account { get; set; } = new();

    public NetworkProxy NetworkProxy { get; set; } = new();
}

public class Account
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public CookieContainer Cookies { get; set; } = new();
}

public class NetworkProxy
{
    public bool? UseProxy { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public ushort Port { get; set; }
}