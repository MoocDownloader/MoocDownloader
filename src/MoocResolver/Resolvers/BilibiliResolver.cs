using MoocResolver.Contracts;

namespace MoocResolver.Resolvers;

/// <summary>
/// Website name: 哔哩哔哩 (゜-゜)つロ 干杯~-bilibili
/// Website address: https://www.bilibili.com/
/// </summary>
public class BilibiliResolver : ResolverBase
{
    public BilibiliResolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override Task<Library> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}