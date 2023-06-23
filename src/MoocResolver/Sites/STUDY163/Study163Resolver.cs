using MoocResolver.Contracts;

namespace MoocResolver.Sites.STUDY163;

/// <summary>
/// Website name: 网易云课堂 - 悄悄变强大
/// Website address: https://study.163.com/
/// </summary>
public class Study163Resolver : ResolverBase
{
    public const string Pattern = @"^(https:\/\/)?study.163.com\/";

    /// <inheritdoc />
    public Study163Resolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override Task<Playlist> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}