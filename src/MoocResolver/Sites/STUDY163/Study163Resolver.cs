using MoocResolver.Contracts;

namespace MoocResolver.Sites.STUDY163;

/// <summary>
/// Website name: 网易云课堂 - 悄悄变强大
/// Website address: https://study.163.com/
/// </summary>
public class Study163Resolver : IResolver
{
    public const string Domain = "study.163.com";

    /// <inheritdoc />
    public bool CanResolve(string link)
    {
        return link.Contains(Domain, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public ResolvedResult Resolve(string link)
    {
        throw new NotImplementedException();
    }
}