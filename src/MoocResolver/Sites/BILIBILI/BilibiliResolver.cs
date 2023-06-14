using MoocResolver.Contracts;

namespace MoocResolver.Sites.BILIBILI;

/// <summary>
/// Website name: 哔哩哔哩 (゜-゜)つロ 干杯~-bilibili
/// Website address: https://www.bilibili.com/
/// </summary>
public class BilibiliResolver : IResolver
{
    public const string Domain = "www.bilibili.com";

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