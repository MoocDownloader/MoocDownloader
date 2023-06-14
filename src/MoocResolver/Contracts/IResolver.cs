namespace MoocResolver.Contracts;

public interface IResolver
{
    bool CanResolve(string link);

    ResolvedResult Resolve(string link);
}