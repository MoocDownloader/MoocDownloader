namespace MoocResolver.Contracts;

public class NetworkProxy
{
    public bool UseProxy { get; set; } = false;

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Host { get; set; } = string.Empty;

    public ushort Port { get; set; }
}