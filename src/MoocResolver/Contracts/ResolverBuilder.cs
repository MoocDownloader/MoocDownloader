using MoocResolver.Exceptions;
using MoocResolver.Sites.BILIBILI;
using MoocResolver.Sites.ICOURSE163;
using MoocResolver.Sites.ICOURSES;
using MoocResolver.Sites.STUDY163;
using MoocResolver.Sites.XUETANGX;
using System.Text.RegularExpressions;

namespace MoocResolver.Contracts;

public class ResolverBuilder
{
    private Type? _matchedType;

    public ResolverBuilder MatchLink(string link)
    {
        if (Regex.IsMatch(link, Course163Resolver.Pattern))
            _matchedType = typeof(Course163Resolver);

        if (Regex.IsMatch(link, BilibiliResolver.Pattern))
            _matchedType = typeof(BilibiliResolver);

        if (Regex.IsMatch(link, CoursesResolver.Pattern))
            _matchedType = typeof(CoursesResolver);

        if (Regex.IsMatch(link, Study163Resolver.Pattern))
            _matchedType = typeof(Study163Resolver);

        if (Regex.IsMatch(link, XuetangxResolver.Pattern))
            _matchedType = typeof(XuetangxResolver);

        return this;
    }

    public IResolver Build(ResolverOption option)
    {
        // Architecture:
        //                                 +-> Using Proxy
        //                                 |
        //            +-> Network Proxy  +-|-> PWD + Username <-+
        //            |                    |                    |
        //            |                    +-> Host + Port      |
        //            |                                         |
        //            |                    +-> Username + PWD   |-+ ResolverOption
        // Resolver +-|-> Credentials    +-|                    |
        //            |                    +-> Cookies          |
        //            |                                         |
        //            |-> Original Link    <-+                <-+
        //            |                      |-+ Dispatcher
        //            +-> Matching Pattern <-+
        //
        if (_matchedType == typeof(Course163Resolver))
            return new Course163Resolver(option);

        if (_matchedType == typeof(BilibiliResolver))
            return new BilibiliResolver(option);

        if (_matchedType == typeof(CoursesResolver))
            return new CoursesResolver(option);

        if (_matchedType == typeof(Study163Resolver))
            return new Study163Resolver(option);

        if (_matchedType == typeof(XuetangxResolver))
            return new XuetangxResolver(option);

        throw new ResolveFailedException(ErrorCodes.Resolver.NotSupport);
    }
}