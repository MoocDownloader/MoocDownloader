namespace MoocResolver.Contracts;

public class ResolverOption
{
    public string Url { get; set; } = string.Empty;

    public Authentication Authentication { get; set; } = new();

    public NetworkProxy NetworkProxy { get; set; } = new();
}