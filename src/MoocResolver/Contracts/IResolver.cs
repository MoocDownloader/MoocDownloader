namespace MoocResolver.Contracts;

public interface IResolver
{
    bool CanResolve();

    Task<Playlist> ResolveAsync();
}