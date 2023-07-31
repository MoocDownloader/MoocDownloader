using MoocResolver.Contracts;

namespace MoocResolver.Sites.ICOURSES;

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
    public override Task<Library> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}