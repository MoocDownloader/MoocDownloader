using MoocResolver.Contracts;

namespace MoocResolver.Resolvers;

/// <summary>
/// Website name: 网易云课堂 - 悄悄变强大
/// Website address: https://study.163.com/
/// </summary>
public class Study163Resolver : ResolverBase
{
    public Study163Resolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override Task<Library> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}