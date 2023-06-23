namespace MoocResolver.Contracts;

public interface IResolver : IDisposable
{
    Task<Playlist> ResolveAsync();
}