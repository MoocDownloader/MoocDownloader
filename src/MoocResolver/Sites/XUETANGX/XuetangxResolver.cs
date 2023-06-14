using MoocResolver.Contracts;

namespace MoocResolver.Sites.XUETANGX;

/// <summary>
/// Website name: 学堂在线 - 精品在线课程学习平台
/// Website address: https://next.xuetangx.com/
/// </summary>
public class XuetangxResolver : IResolver
{
    public const string Domain = "next.xuetangx.com";

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