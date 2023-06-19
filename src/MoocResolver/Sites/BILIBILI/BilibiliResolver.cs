using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Sites.BILIBILI;

/// <summary>
/// Website name: 哔哩哔哩 (゜-゜)つロ 干杯~-bilibili
/// Website address: https://www.bilibili.com/
/// </summary>
public class BilibiliResolver : ResolverBase
{
    public const string Domain = "www.bilibili.com";

    /// <inheritdoc />
    public BilibiliResolver(string link, CookieCollection cookies) : base(link, cookies)
    {
    }

    /// <inheritdoc />
    public override bool CanResolve()
    {
        return Link.Contains(Domain, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public override Task<Playlist> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}