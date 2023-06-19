using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Sites.ICOURSES;

/// <summary>
/// Website name: 爱课程
/// Website address: https://www.icourses.cn/home/
/// </summary>
public class CoursesResolver : ResolverBase
{
    public const string Domain = "www.icourses.cn";

    /// <inheritdoc />
    public CoursesResolver(string link, CookieCollection cookies) : base(link, cookies)
    {
    }

    /// <inheritdoc />
    public override bool CanResolve()
    {
        return Link.Contains(Domain, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public override Task<Playlist> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}