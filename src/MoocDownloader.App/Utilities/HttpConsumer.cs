using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MoocDownloader.App.Utilities
{
    /// <summary>
    /// HTTP consumer.
    /// </summary>
    public class HttpConsumer
    {
        private Encoding        _encoding     = Encoding.Default;
        private Encoding        _postEncoding = Encoding.Default;
        private HttpWebRequest  _request;
        private HttpWebResponse _response;
        private IPEndPoint      _ipEndPoint;


        /// <summary>
        /// Send request.
        /// </summary>
        /// <param name="request">the request.</param>
        /// <returns>the response.</returns>
        public HttpResponse Send(HttpRequest request)
        {
            var result = new HttpResponse();
            try
            {
                SetRequest(request);
            }
            catch (Exception ex)
            {
                return new HttpResponse()
                {
                    Cookie            = string.Empty,
                    Headers           = null,
                    Content           = ex.Message,
                    StatusDescription = ex.Message
                };
            }

            try
            {
                using (_response = (HttpWebResponse) _request.GetResponse())
                {
                    GetData(request, result);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (_response = (HttpWebResponse) ex.Response)
                    {
                        GetData(request, result);
                    }
                }
                else
                {
                    result.Content = ex.Message;
                }
            }
            catch (Exception ex)
            {
                result.Content = ex.Message;
            }

            if (request.IsToLower) result.Content = result.Content.ToLower();

            if (request.IsReset)
            {
                _request  = null;
                _response = null;
            }

            return result;
        }

        /// <summary>
        /// Send request.
        /// </summary>
        /// <param name="request">request.</param>
        /// <returns>response.</returns>
        public Task<HttpResponse> SendAsync(HttpRequest request)
        {
            return Task<HttpResponse>.Factory.StartNew(() => Send(request));
        }

        /// <summary>
        /// create http consumer.
        /// </summary>
        /// <returns>HTTP consumer.</returns>
        public static HttpConsumer Create()
        {
            return new HttpConsumer();
        }

        /// <summary>
        /// Get http response data.
        /// </summary>
        /// <param name="request">HTTP request.</param>
        /// <param name="response">HTTP response.</param>
        private void GetData(HttpRequest request, HttpResponse response)
        {
            if (_response is null)
            {
                return;
            }

            response.StatusCode        = _response.StatusCode;
            response.StatusDescription = _response.StatusDescription;
            response.Headers           = _response.Headers;
            response.ResponseUri       = _response.ResponseUri.ToString();

            if (_response.Cookies               != null) response.CookieCollection = _response.Cookies;
            if (_response.Headers["set-cookie"] != null) response.Cookie           = _response.Headers["set-cookie"];

            var ResponseByte = GetByte();

            if (ResponseByte != null && ResponseByte.Length > 0)
            {
                SetEncoding(request, response, ResponseByte);
                response.Content = _encoding.GetString(ResponseByte);
            }
            else
            {
                response.Content = string.Empty;
            }
        }

        /// <summary>
        /// Set encoding.
        /// </summary>
        /// <param name="request">HttpItem</param>
        /// <param name="response">HttpResult</param>
        /// <param name="ResponseByte">byte[]</param>
        private void SetEncoding(HttpRequest request, HttpResponse response, byte[] ResponseByte)
        {
            if (request.ResultType == ResultType.Byte) response.ResultBytes = ResponseByte;

            if (_encoding is null)
            {
                var meta = Regex.Match(Encoding.Default.GetString(ResponseByte), "<meta[^<]*charset=([^<]*)[\"']",
                                       RegexOptions.IgnoreCase);
                var c = string.Empty;
                if (meta.Groups.Count > 0)
                {
                    c = meta.Groups[1].Value.ToLower().Trim();
                }

                if (c.Length > 2)
                {
                    try
                    {
                        _encoding = Encoding.GetEncoding(c.Replace("\"", string.Empty).Replace("'", "").Replace(";", "")
                                                          .Replace("iso-8859-1", "gbk").Trim());
                    }
                    catch
                    {
                        _encoding = string.IsNullOrEmpty(_response.CharacterSet)
                            ? Encoding.UTF8
                            : Encoding.GetEncoding(_response.CharacterSet);
                    }
                }
                else
                {
                    _encoding = string.IsNullOrEmpty(_response.CharacterSet)
                        ? Encoding.UTF8
                        : Encoding.GetEncoding(_response.CharacterSet);
                }
            }
        }

        /// <summary>
        /// Get http bytes.
        /// </summary>
        /// <returns></returns>
        private byte[] GetByte()
        {
            byte[] ResponseByte;
            using (var _stream = new MemoryStream())
            {
                if (_response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    new GZipStream(_response.GetResponseStream() ?? Stream.Null, CompressionMode.Decompress).CopyTo(
                        _stream, 1024);
                }
                else
                {
                    _response.GetResponseStream()?.CopyTo(_stream, 1024);
                }

                ResponseByte = _stream.ToArray();
            }

            return ResponseByte;
        }


        /// <summary>
        /// set request parameter.
        /// </summary>
        ///<param name="request">the request parameters.</param>
        private void SetRequest(HttpRequest request)
        {
            SetCer(request);
            if (request.IPEndPoint != null)
            {
                _ipEndPoint                                  = request.IPEndPoint;
                _request.ServicePoint.BindIPEndPointDelegate = BindIPEndPointCallback;
            }

            if (request.Header != null && request.Header.Count > 0)
                foreach (var key in request.Header.AllKeys)
                {
                    _request.Headers.Add(key, request.Header[key]);
                }

            SetProxy(request);
            if (request.ProtocolVersion != null) _request.ProtocolVersion = request.ProtocolVersion;
            _request.ServicePoint.Expect100Continue = request.Expect100Continue;

            _request.Method           = Enum.GetName(typeof(HttpMethod), request.Method) ?? "GET";
            _request.Timeout          = request.Timeout;
            _request.KeepAlive        = request.KeepAlive;
            _request.ReadWriteTimeout = request.ReadWriteTimeout;
            if (!string.IsNullOrWhiteSpace(request.Host))
            {
                _request.Host = request.Host;
            }

            if (request.IfModifiedSince != null) _request.IfModifiedSince = Convert.ToDateTime(request.IfModifiedSince);
            if (request.Date != null)
            {
                _request.Date = Convert.ToDateTime(request.Date);
            }

            _request.Accept      = request.Accept;
            _request.ContentType = request.ContentType;
            _request.UserAgent   = request.UserAgent;
            _encoding            = request.Encoding;
            _request.Credentials = request.ICredentials;

            SetCookie(request);

            _request.Referer           = request.Referer;
            _request.AllowAutoRedirect = request.AllowAutoRedirect;

            if (request.MaximumAutomaticRedirections > 0)
            {
                _request.MaximumAutomaticRedirections = request.MaximumAutomaticRedirections;
            }

            SetPostData(request);
            if (request.Connectionlimit > 0) _request.ServicePoint.ConnectionLimit = request.Connectionlimit;
        }

        /// <summary>
        /// set certificate
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        private void SetCer(HttpRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.CerPath))
            {
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;

                _request = (HttpWebRequest) WebRequest.Create(request.Url);
                SetCerList(request);

                _request.ClientCertificates.Add(new X509Certificate(request.CerPath));
            }
            else
            {
                _request = (HttpWebRequest) WebRequest.Create(request.Url);
                SetCerList(request);
            }
        }

        /// <summary>
        /// set certificate
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        private void SetCerList(HttpRequest request)
        {
            if (request.ClentCertificates != null && request.ClentCertificates.Count > 0)
            {
                foreach (var c in request.ClentCertificates)
                {
                    _request.ClientCertificates.Add(c);
                }
            }
        }

        /// <summary>
        /// Set cookie.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        private void SetCookie(HttpRequest request)
        {
            if (!string.IsNullOrEmpty(request.Cookie)) _request.Headers[HttpRequestHeader.Cookie] = request.Cookie;

            if (request.CookieType == CookieType.CookieCollection)
            {
                _request.CookieContainer = new CookieContainer();
                if (request.CookieCollection != null && request.CookieCollection.Count > 0)
                {
                    if (request.CookieCollection.Count > 20)
                    {
                        _request.CookieContainer.PerDomainCapacity = request.CookieCollection.Count;
                    }

                    _request.CookieContainer.Add(request.CookieCollection);
                }
            }
        }

        /// <summary>
        /// Set post body data.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        private void SetPostData(HttpRequest request)
        {
            if (!_request.Method.Trim().ToLower().Contains("get"))
            {
                if (request.PostEncoding != null)
                {
                    _postEncoding = request.PostEncoding;
                }

                byte[] buffer = null;
                if (request.PostBodyType  == PostBodyType.Bytes
                 && request.PostBodyBytes != null && request.PostBodyBytes.Length > 0)
                {
                    buffer = request.PostBodyBytes;
                }
                else if (request.PostBodyType == PostBodyType.FilePath &&
                         !string.IsNullOrWhiteSpace(request.PostBodyText))
                {
                    var r = new StreamReader(request.PostBodyText, _postEncoding);
                    buffer = _postEncoding.GetBytes(r.ReadToEnd());
                    r.Close();
                }
                else if (!string.IsNullOrWhiteSpace(request.PostBodyText))
                {
                    buffer = _postEncoding.GetBytes(request.PostBodyText);
                }

                if (buffer != null)
                {
                    _request.ContentLength = buffer.Length;
                    _request.GetRequestStream().Write(buffer, 0, buffer.Length);
                }
                else
                {
                    _request.ContentLength = 0;
                }
            }
        }

        /// <summary>
        /// Set proxy.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        private void SetProxy(HttpRequest request)
        {
            var isIeProxy = false;
            if (!string.IsNullOrWhiteSpace(request.ProxyIp))
            {
                isIeProxy = request.ProxyIp.ToLower().Contains("ieproxy");
            }

            if (!string.IsNullOrWhiteSpace(request.ProxyIp) && !isIeProxy)
            {
                if (request.ProxyIp.Contains(":"))
                {
                    var plist = request.ProxyIp.Split(':');
                    var myProxy = new WebProxy(plist[0].Trim(), Convert.ToInt32(plist[1].Trim()))
                    {
                        Credentials = new NetworkCredential(request.ProxyUserName, request.ProxyPwd)
                    };

                    _request.Proxy = myProxy;
                }
                else
                {
                    var myProxy = new WebProxy(request.ProxyIp, false)
                    {
                        Credentials = new NetworkCredential(request.ProxyUserName, request.ProxyPwd)
                    };
                    _request.Proxy = myProxy;
                }
            }
            else if (isIeProxy)
            {
            }
            else
            {
                _request.Proxy = request.WebProxy;
            }
        }

        /// <summary>
        /// Certificate validation.
        /// </summary>
        private static bool CheckValidationResult(object          sender,
                                                  X509Certificate certificate,
                                                  X509Chain       chain,
                                                  SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// set http using ip and port.
        /// </summary>
        private IPEndPoint BindIPEndPointCallback(ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount)
        {
            return _ipEndPoint; // IP Endpoint.
        }
    }

    /// <summary>
    /// Http request.
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// Url of HTTP request.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// HTTP method.
        /// </summary>
        public HttpMethod Method { get; set; } = HttpMethod.GET;

        /// <summary>
        /// timeout.
        /// </summary>
        public int Timeout { get; set; } = 100000;

        /// <summary>
        /// Read and write post data timeout.
        /// </summary>
        public int ReadWriteTimeout { get; set; } = 30000;

        /// <summary>
        /// set host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        ///  is keep alive.
        /// </summary>
        public bool KeepAlive { get; set; } = true;

        /// <summary>
        /// set accept.
        /// </summary>
        public string Accept { get; set; }

        /// <summary>
        /// Content type.
        /// </summary>
        public string ContentType { get; set; } = "text/html";

        /// <summary>
        /// User-Agent
        /// </summary>
        public string UserAgent { get; set; } =
            "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36";

        /// <summary>
        /// response encoding.
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// post data type.
        /// </summary>
        public PostBodyType PostBodyType { get; set; } = PostBodyType.String;

        /// <summary>
        /// Post body string.
        /// </summary>
        public string PostBodyText { get; set; }

        /// <summary>
        /// Post body bytes.
        /// </summary>
        public byte[] PostBodyBytes { get; set; }

        /// <summary>
        /// Cookie collection
        /// </summary>
        public CookieCollection CookieCollection { get; set; } = new CookieCollection();

        /// <summary>
        /// Cookie.
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// Referer.
        /// </summary>
        public string Referer { get; set; }

        /// <summary>
        /// Certificate path.
        /// </summary>
        public string CerPath { get; set; }

        /// <summary>
        /// Set proxy.
        /// </summary>
        public WebProxy WebProxy { get; set; }

        /// <summary>
        /// Is to lower.
        /// </summary>
        public bool IsToLower { get; set; } = false;

        /// <summary>
        /// Get or set HTTP date.
        /// </summary>
        public DateTime? Date { get; set; } = null;

        /// <summary>
        /// allow HTTP auto redirect.
        /// </summary>
        public bool AllowAutoRedirect { get; set; } = false;

        /// <summary>
        /// Maximum connection.
        /// </summary>
        public int Connectionlimit { get; set; } = 1024;

        /// <summary>
        /// proxy user name.
        /// </summary>
        public string ProxyUserName { get; set; }

        /// <summary>
        /// proxy password.
        /// </summary>
        public string ProxyPwd { get; set; }

        /// <summary>
        /// proxy ip.
        /// </summary>
        public string ProxyIp { get; set; }

        /// <summary>
        /// response return type.
        /// </summary>
        public ResultType ResultType { get; set; } = ResultType.String;

        /// <summary>
        /// Headers.
        /// </summary>
        public WebHeaderCollection Header { get; set; } = new WebHeaderCollection();

        /// <summary>
        /// Get or set HTTP version.
        /// </summary>
        public Version ProtocolVersion { get; set; }

        /// <summary>
        /// use 100-Continue.
        /// </summary>
        public bool Expect100Continue { get; set; } = false;

        /// <summary>
        /// Certificate
        /// </summary>
        public X509CertificateCollection ClentCertificates { get; set; }

        /// <summary>
        /// Post encoding.
        /// </summary>
        public Encoding PostEncoding { get; set; }

        /// <summary>
        /// Cookie type.
        /// </summary>
        public CookieType CookieType { get; set; } = CookieType.String;

        /// <summary>
        /// Get or set the requested authentication information.
        /// </summary>
        public ICredentials ICredentials { get; set; } = CredentialCache.DefaultCredentials;

        /// <summary>
        /// Set the maximum number of redirects that the request will request.
        /// </summary>
        public int MaximumAutomaticRedirections { get; set; }

        /// <summary>
        /// Get or set IfModifiedSince.
        /// </summary>
        public DateTime? IfModifiedSince { get; set; } = null;

        #region ip-port

        /// <summary>
        /// Set the local ip and port.
        /// </summary>]
        /// <example>
        /// item.IPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.1.1"),80);
        /// </example>
        public IPEndPoint IPEndPoint { get; set; } = null;

        #endregion

        /// <summary>
        /// Whether to reset request and response properties. Not reset by default.
        /// </summary>
        public bool IsReset { get; set; } = false;

        /// <summary>
        /// Set URL query string.
        /// </summary>
        /// <param name="key">Key of query string.</param>
        /// <param name="value">Value of query string.</param>
        /// <returns>HTTP request.</returns>
        public HttpRequest SetQueryParameter(string key, string value)
        {
            if (Url.Contains("?") && Url.Contains("=") && Url.Contains("&"))
            {
                Url = $"{Url}&{key.Trim()}={HttpUtility.UrlEncode(value)}";
            }
            else if (Url.Contains("?") && Url.Contains("="))
            {
                Url = $"{Url}&{key.Trim()}={HttpUtility.UrlEncode(value)}";
            }
            else if (Url.EndsWith("?"))
            {
                Url = $"{Url}{key.Trim()}={HttpUtility.UrlEncode(value)}";
            }
            else
            {
                Url = $"{Url}?{key.Trim()}={HttpUtility.UrlEncode(value)}";
            }

            return this;
        }

        /// <summary>
        /// Set URL query string.
        /// </summary>
        /// <param name="collection">Query string collection.</param>
        /// <returns>HTTP request.</returns>
        public HttpRequest SetQueryParameter(IDictionary<string, string> collection)
        {
            foreach (var query in collection)
            {
                SetQueryParameter(query.Key, query.Value);
            }

            return this;
        }

        /// <summary>
        /// Set url.
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns>HTTP request.</returns>
        public HttpRequest SetUrl(string url)
        {
            Url = url;

            return this;
        }

        /// <summary>
        /// Set the HTTP method.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <returns>HTTP Request.</returns>
        public HttpRequest SetMethod(HttpMethod method)
        {
            Method = method;

            return this;
        }

        /// <summary>
        /// Set the HTTP method.
        /// </summary>
        /// <param name="method">HTTP method.</param>
        /// <returns>HTTP Request.</returns>
        public HttpRequest SetMethod(string method)
        {
            switch (method)
            {
                case "CONNECT":
                    Method = HttpMethod.CONNECT;
                    break;
                case "DELETE":
                    Method = HttpMethod.DELETE;
                    break;
                case "GET":
                    Method = HttpMethod.GET;
                    break;
                case "HEAD":
                    Method = HttpMethod.HEAD;
                    break;
                case "OPTIONS":
                    Method = HttpMethod.OPTIONS;
                    break;
                case "PATCH":
                    Method = HttpMethod.PATCH;
                    break;
                case "POST":
                    Method = HttpMethod.POST;
                    break;
                case "PUT":
                    Method = HttpMethod.PUT;
                    break;
                case "TRACE":
                    Method = HttpMethod.TRACE;
                    break;
            }

            return this;
        }

        /// <summary>
        /// Add header.
        /// </summary>
        /// <param name="name">Key of request header.</param>
        /// <param name="value">Value of request header.</param>
        /// <returns>HTTP Request.</returns>
        public HttpRequest AddHeader(string name, string value)
        {
            Header.Add(name, value);

            return this;
        }

        /// <summary>
        /// Create HTTP Request.
        /// </summary>
        /// <returns>HTTP Request.</returns>
        public static HttpRequest Create()
        {
            return new HttpRequest();
        }
    }

    /// <summary>
    /// HTTP Response.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// Cookie of HTTP response.
        /// </summary>
        public string Cookie { get; set; }

        /// <summary>
        /// Cookie Collection.
        /// </summary>
        public CookieCollection CookieCollection { get; set; }

        /// <summary>
        /// Response string. Only return string data when the ResultType property is string, otherwise empty.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Response byte array. Only return byte data when the ResultType property is Byte, otherwise empty.
        /// </summary>
        public byte[] ResultBytes { get; set; }

        /// <summary>
        /// Headers.
        /// </summary>
        public WebHeaderCollection Headers { get; set; }

        /// <summary>
        /// Descripition of return status.
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// HTTP status code. Default is OK.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Last visit url.
        /// </summary>
        public string ResponseUri { get; set; }

        /// <summary>
        /// The redirected url.
        /// </summary>
        public string RedirectUrl
        {
            get
            {
                try
                {
                    if (Headers != null && Headers.Count > 0)
                    {
                        if (Headers.AllKeys.Any(k => k.ToLower().Contains("location")))
                        {
                            var baseurl     = Headers["location"].Trim();
                            var locationurl = baseurl.ToLower();

                            if (string.IsNullOrWhiteSpace(locationurl)) return baseurl;

                            if (locationurl.StartsWith("http://") || locationurl.StartsWith("https://")) return baseurl;

                            return new Uri(new Uri(ResponseUri), baseurl).AbsoluteUri;
                        }
                    }
                }
                catch
                {
                    return string.Empty;
                }

                return string.Empty;
            }
        }
    }

    /// <summary>
    /// The return type.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Represents that only strings are returned.
        /// </summary>
        String,

        /// <summary>
        /// Represents that both of string and byte are returned.
        /// </summary>
        Byte
    }

    /// <summary>
    /// The post request body data type. The default is string type.
    /// </summary>
    public enum PostBodyType
    {
        /// <summary>
        /// The string type. The encoding need not be set when the type is a string type.
        /// </summary>
        String,

        /// <summary>
        /// The byte array type. The encoding parameter can be null.
        /// </summary>
        Bytes,

        /// <summary>
        /// Post a file. The PostBody property should be set to the absolute path of the file.
        /// </summary>
        FilePath
    }

    /// <summary>
    /// Cookie return type.
    /// </summary>
    public enum CookieType
    {
        /// <summary>
        /// The string type.
        /// </summary>
        String,

        /// <summary>
        /// The collection of cookie.
        /// </summary>
        CookieCollection
    }

    /// <summary>
    /// HTTP request method.
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// CONNECT.
        /// </summary>
        CONNECT,

        /// <summary>
        /// DELETE.
        /// </summary>
        DELETE,

        /// <summary>
        /// GET.
        /// </summary>
        GET,

        /// <summary>
        /// HEAD.
        /// </summary>
        HEAD,

        /// <summary>
        /// OPTIONS.
        /// </summary>
        OPTIONS,

        /// <summary>
        /// PATCH.
        /// </summary>
        PATCH,

        /// <summary>
        /// POST.
        /// </summary>
        POST,

        /// <summary>
        /// PUT.
        /// </summary>
        PUT,

        /// <summary>
        /// TRACE.
        /// </summary>
        TRACE,
    }
}