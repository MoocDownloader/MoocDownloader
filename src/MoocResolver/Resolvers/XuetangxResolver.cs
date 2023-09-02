using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Resolvers;

/// <summary>
/// Website name: 学堂在线 - 精品在线课程学习平台
/// Website address: https://next.xuetangx.com/
/// </summary>
public class XuetangxResolver : WebsiteResolverBase
{
    /// <inheritdoc />
    public XuetangxResolver(WebsiteResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override bool AuthenticationRequired { get; set; } = true;

    /// <inheritdoc />
    public override Task<Library> ResolveAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override Task<CookieCollection> LoginAsync()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public override Task<bool> CheckAsync()
    {
        throw new NotImplementedException();
    }
}