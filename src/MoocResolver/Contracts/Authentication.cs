namespace MoocResolver.Contracts;

public class Authentication
{
    public AuthenticationType AuthenticationType { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Cookies { get; set; } = string.Empty;
}