using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Resolvers;

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