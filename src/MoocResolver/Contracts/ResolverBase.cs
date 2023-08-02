using CefSharp;
using CefSharp.Core;
using CefSharp.OffScreen;
using MoocResolver.Exceptions;
using Serilog;
using System.Net;
using System.Net.Http.Headers;

namespace MoocResolver.Contracts;

public abstract class ResolverBase : IResolver
{
    public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/115.0";
    public const string StartPage = "chrome://version";

    protected HttpClient? HttpClient { get; set; }
    protected ChromiumWebBrowser? Browser { get; set; }

    protected readonly ILogger Logger;
    protected readonly ResolverOption Option;

    protected ResolverBase(ResolverOption option)
    {
        Option = option;
        Logger = Log.ForContext(typeof(ResolverBase));
    }

    /// <inheritdoc />
    public abstract bool AuthenticationRequired { get; set; }

    /// <inheritdoc />
    public abstract Task<Library> ResolveAsync();

    /// <inheritdoc />
    public abstract Task<CookieCollection> LoginAsync();

    /// <inheritdoc />
    public abstract Task<bool> CheckAsync();

    protected virtual HttpClientHandler GetHttpClientHandler(bool useCookies, bool autoRedirect = false)
    {
        return new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            AllowAutoRedirect = autoRedirect,
            UseCookies = useCookies,
            CookieContainer = Cookies,
        };
    }

    protected virtual void InitializeHttpClient(
        bool useCookies = true,
        bool autoRedirect = false,
        TimeSpan timeout = new())
    {
        var httpClientHandler = GetHttpClientHandler(useCookies, autoRedirect);

        HttpClient = new HttpClient(httpClientHandler)
        {
            Timeout = timeout == TimeSpan.Zero ? TimeSpan.FromMilliseconds(int.MaxValue) : timeout,
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

    protected virtual async Task InitializeBrowserAsync(bool disableImage = true)
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
            address: StartPage,
            browserSettings: browserSettings,
            onAfterBrowserCreated: OnAfterBrowserCreated);

        Browser.LoadingStateChanged += BrowserOnLoadingStateChanged;
        Browser.AddressChanged += BrowserOnAddressChanged;
        Browser.BrowserInitialized += BrowserOnBrowserInitialized;

        var response = await Browser.WaitForInitialLoadAsync();

        if (!response.Success)
        {
            throw new ResolveFailedException(ErrorCodes.Browser.InitializationFailed);
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

    protected virtual long CurrentTimestamp(bool isMilliseconds = true) =>
        isMilliseconds
            ? DateTimeOffset.Now.ToUnixTimeMilliseconds()
            : DateTimeOffset.Now.ToUnixTimeSeconds();

    protected virtual Account Account => Option.Account;

    protected virtual CookieContainer Cookies => Account.Cookies;

    protected virtual NetworkProxy NetworkProxy => Option.NetworkProxy;

    /// <inheritdoc />
    public virtual void Dispose()
    {
        HttpClient?.Dispose();
        Browser?.Dispose();
    }
}