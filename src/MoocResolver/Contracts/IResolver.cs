namespace MoocResolver.Contracts;

public interface IResolver : IDisposable
{
    Task<Library> ResolveAsync();
}