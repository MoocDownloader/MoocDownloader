using System.Net;

namespace MoocResolver.Contracts;

public interface IWebsiteResolver : IDisposable
{
    bool AuthenticationRequired { get; set; }

    Task<Library> ResolveAsync();

    Task<CookieCollection> LoginAsync();

    Task<bool> CheckAsync();
}