using System.Net;

namespace MoocResolver.Contracts;

public interface IResolver : IDisposable
{
    bool AuthenticationRequired { get; set; }

    Task<Library> ResolveAsync();

    Task<CookieCollection> LoginAsync();

    Task<bool> CheckAsync();
}