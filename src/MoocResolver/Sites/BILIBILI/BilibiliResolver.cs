using MoocResolver.Contracts;

namespace MoocResolver.Sites.BILIBILI;

/// <summary>
/// Website name: 哔哩哔哩 (゜-゜)つロ 干杯~-bilibili
/// Website address: https://www.bilibili.com/
/// </summary>
public class BilibiliResolver : ResolverBase
{
    public const string Pattern = @"^(https:\/\/)?www.bilibili.com\/";

    /// <inheritdoc />
    public BilibiliResolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override Task<Playlist> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}