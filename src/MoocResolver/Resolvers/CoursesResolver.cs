using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Resolvers;

/// <summary>
/// Website name: 爱课程
/// Website address: https://www.icourses.cn/home/
/// </summary>
public class CoursesResolver : ResolverBase
{
    public const string Pattern = @"^(https:\/\/)?www.icourses.cn\/";

    /// <inheritdoc />
    public CoursesResolver(ResolverOption option) : base(option)
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