using MoocResolver.Contracts;
using System.Net;

namespace MoocResolver.Sites.STUDY163;

/// <summary>
/// Website name: 网易云课堂 - 悄悄变强大
/// Website address: https://study.163.com/
/// </summary>
public class Study163Resolver : ResolverBase
{
    public const string Domain = "study.163.com";

    /// <inheritdoc />
    public Study163Resolver(string link, CookieCollection cookies) : base(link, cookies)
    {
    }

    /// <inheritdoc />
    public override bool CanResolve()
    {
        return Link.Contains(Domain, StringComparison.OrdinalIgnoreCase);
    }

    /// <inheritdoc />
    public override Task<ResolvedResult> ResolveAsync()
    {
        var cookieKeys = new[] { "S_INFO", "P_INFO", "STUDY_INFO", "STUDY_SESS", "STUDY_PERSIST" };
        throw new NotImplementedException();
    }
}