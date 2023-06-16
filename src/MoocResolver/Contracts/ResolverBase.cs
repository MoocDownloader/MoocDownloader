using System.Net;

namespace MoocResolver.Contracts;

public abstract class ResolverBase : IResolver, IDisposable
{
    public const string UserAgent =
        "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) " +
        "Chrome/114.0.0.0 Safari/537.36";

    protected HttpClient? HttpClient { get; set; }

    protected readonly string Link;
    protected readonly CookieCollection Cookies;

    protected ResolverBase(string link, CookieCollection cookies)
    {
        Link = link;
        Cookies = cookies;
    }

    /// <inheritdoc />
    public abstract bool CanResolve();

    /// <inheritdoc />
    public abstract Task<ResolvedResult> ResolveAsync();

    protected CookieContainer GetCookieContainer()
    {
        var cookieContainer = new CookieContainer();

        foreach (Cookie cookie in Cookies)
        {
            cookieContainer.Add(cookie);
        }

        return cookieContainer;
    }

    protected void InitialHttpClient(bool useCookies = true)
    {
        HttpClient ??= new HttpClient(new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = false,
            UseCookies = useCookies,
            CookieContainer = GetCookieContainer(),
        })
        {
            Timeout = TimeSpan.FromMilliseconds(int.MaxValue)
        };
    }

    /// <inheritdoc />
    public void Dispose()
    {
        HttpClient?.Dispose();
    }
}