using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Sites.XUETANGX;

/// <summary>
/// Website name: 学堂在线 - 精品在线课程学习平台
/// Website address: https://next.xuetangx.com/
/// </summary>
public class XuetangxResolver : ResolverBase
{
    public const string Domain = "next.xuetangx.com";

    /// <inheritdoc />
    public XuetangxResolver(string link, CookieCollection cookies) : base(link, cookies)
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