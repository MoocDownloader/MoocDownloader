using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Resolvers;

/// <summary>
/// Website name: 爱课程
/// Website address: https://www.icourses.cn/home/
/// </summary>
public class CoursesResolver : ResolverBase
{
    public CoursesResolver(ResolverOption option) : base(option)
    {
    }

    /// <inheritdoc />
    public override Task<Library> ResolveAsync()
    {
        throw new NotImplementedException();
    }
}