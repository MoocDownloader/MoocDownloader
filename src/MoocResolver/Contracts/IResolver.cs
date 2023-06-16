namespace MoocResolver.Contracts;

public interface IResolver
{
    bool CanResolve();

    Task<ResolvedResult> ResolveAsync();
}