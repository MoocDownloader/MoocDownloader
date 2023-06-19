using CefSharp;
using CefSharp.Core;
using CefSharp.OffScreen;
using CefSharp.Web;
using Serilog;
using System.Net;
using System.Net.Http.Headers;

namespace MoocResolver.Contracts;

public abstract class ResolverBase : IResolver, IDisposable
{
    public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/112.0";

    protected HttpClient? HttpClient { get; set; }
    protected ChromiumWebBrowser? Browser { get; set; }

    protected readonly string Link;
    protected readonly CookieCollection Cookies;
    protected readonly ILogger Logger;

    protected ResolverBase(string link, CookieCollection cookies)
    {
        Link = link;
        Cookies = cookies;
        Logger = Log.ForContext(typeof(ResolverBase));
    }

    /// <inheritdoc />
    public abstract bool CanResolve();

    /// <inheritdoc />
    public abstract Task<Playlist> ResolveAsync();

    protected virtual CookieContainer GetCookieContainer()
    {
        var cookieContainer = new CookieContainer();

        foreach (System.Net.Cookie cookie in Cookies)
        {
            cookieContainer.Add(cookie);
        }

        return cookieContainer;
    }

    protected virtual void InitialHttpClient(bool useCookies = true, TimeSpan timeout = new())
    {
        var httpClientHandler = new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = false,
            UseCookies = useCookies,
            CookieContainer = GetCookieContainer(),
        };

        HttpClient = new HttpClient(httpClientHandler)
        {
            Timeout = timeout == TimeSpan.Zero ? TimeSpan.FromMilliseconds(int.MaxValue) : timeout
        };
    }

    protected virtual void AddHttpHeaders(HttpRequestMessage request, NameValueHeaderValue[] headers)
    {
        foreach (var header in headers)
        {
            request.Headers.Add(header.Name, header.Value);
        }

        request.Headers.Add("Accept", "*/*");
        request.Headers.Add("Accept-Language", "en-US,en;q=0.5");
        request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
        request.Headers.Add("DNT", "1");
        request.Headers.Add("Connection", "keep-alive");
        request.Headers.Add("Upgrade-Insecure-Requests", "1");
        request.Headers.Add("Sec-Fetch-Dest", "empty");
        request.Headers.Add("Sec-Fetch-Mode", "cors");
        request.Headers.Add("Sec-Fetch-Site", "same-origin");
        request.Headers.Add("User-Agent", UserAgent);
    }

    protected virtual async Task InitialBrowserAsync(bool disableImage = true)
    {
        var localData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var cachePath = Path.Combine(localData, @"MoocDownloader\Cache");
        var userDataPath = Path.Combine(localData, @"MoocDownloader\UserData");
        var cefSettings = new CefSettings
        {
            CachePath = cachePath,
            UserDataPath = userDataPath,
            UserAgent = UserAgent,
        };
        var success = await CefSharp.Cef.InitializeAsync(
            settings: cefSettings,
            performDependencyCheck: true,
            browserProcessHandler: null);

        if (!success)
        {
            // Can not initial the CEF browser.
            throw new ApplicationException();
        }

        var browserSettings = ObjectFactory.CreateBrowserSettings(autoDispose: true);

        // Disable loading images.
        browserSettings.ImageLoading = disableImage ? CefState.Disabled : CefState.Default;

        Browser = new ChromiumWebBrowser(
            browserSettings: browserSettings,
            onAfterBrowserCreated: OnAfterBrowserCreated,
            html: new HtmlString("<h1>hello</h1>"));

        Browser.LoadingStateChanged += BrowserOnLoadingStateChanged;
        Browser.AddressChanged += BrowserOnAddressChanged;
        Browser.BrowserInitialized += BrowserOnBrowserInitialized;

        var response = await Browser.WaitForInitialLoadAsync();

        if (!response.Success)
        {
            throw new ApplicationException();
        }
    }

    #region CEF Browser events

    protected virtual void BrowserOnBrowserInitialized(object? sender, EventArgs e)
    {
#if DEBUG
        Logger.Debug("Browser is initialized.");
#endif
    }

    protected virtual void BrowserOnAddressChanged(object? sender, AddressChangedEventArgs e)
    {
#if DEBUG
        Logger.Debug("Browser's address is changed to {address}", e.Address);
#endif
    }

    protected virtual void BrowserOnLoadingStateChanged(object? sender, LoadingStateChangedEventArgs e)
    {
#if DEBUG
        if (e.IsLoading)
        {
            Logger.Debug("Browser is loading.");
        }
#endif
    }

    protected virtual void OnAfterBrowserCreated(IBrowser browser)
    {
#if DEBUG
        Logger.Debug("Browser is created.");
#endif
    }

    #endregion

    /// <inheritdoc />
    public virtual void Dispose()
    {
        HttpClient?.Dispose();
        Browser?.Dispose();
    }
}