using System.Net;
using MoocResolver.Contracts;

namespace MoocResolver.Resolvers;

/// <summary>
/// Website name: 哔哩哔哩 (゜-゜)つロ 干杯~-bilibili
/// Website address: https://www.bilibili.com/
/// </summary>
public class BilibiliResolver : WebsiteResolverBase
{
    public const string Pattern = @"^(https:\/\/)?www.bilibili.com\/";

    /// <inheritdoc />
    public BilibiliResolver(WebsiteResolverOption option) : base(option)
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