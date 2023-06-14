using MoocResolver.Contracts;

namespace MoocResolver.Sites.ICOURSE163;

/// <summary>
/// Website name: 中国大学MOOC(慕课)_国家精品课程在线学习平台
/// Website address: https://www.icourse163.org/
/// </summary>
public class Course163Resolver : IResolver
{
    public const string Domain = "www.icourse163.org";

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