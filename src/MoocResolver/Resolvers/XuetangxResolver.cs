using MoocResolver.Contracts;

namespace MoocResolver.Resolvers;

/// <summary>
/// Website name: 学堂在线 - 精品在线课程学习平台
/// Website address: https://next.xuetangx.com/
/// </summary>
public class XuetangxResolver : ResolverBase
{
    public XuetangxResolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override Task<Library> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}