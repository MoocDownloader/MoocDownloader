using MoocResolver.Contracts;

namespace MoocResolver.Sites.ICOURSES;

/// <summary>
/// Website name: 爱课程
/// Website address: https://www.icourses.cn/home/
/// </summary>
public class CoursesResolver : IResolver
{
    public const string Domain = "www.icourses.cn";

    /// <inheritdoc />
    public bool CanResolve(string link)
    {
        return link.Contains(Domain, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public ResolvedResult Resolve(string link)
    {
        throw new NotImplementedException();
    }
}