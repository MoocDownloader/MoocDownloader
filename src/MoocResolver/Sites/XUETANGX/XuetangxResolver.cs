using MoocResolver.Contracts;

namespace MoocResolver.Sites.XUETANGX;

/// <summary>
/// Website name: 学堂在线 - 精品在线课程学习平台
/// Website address: https://next.xuetangx.com/
/// </summary>
public class XuetangxResolver : ResolverBase
{
    public const string Pattern = @"^(https:\/\/)?next.xuetangx.com\/";

    /// <inheritdoc />
    public XuetangxResolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override Task<Library> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}