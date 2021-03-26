using HtmlAgilityPack;
using MoocDownloader.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cookie = System.Net.Cookie;

namespace MoocDownloader.App.Mooc
{
    /// <summary>
    /// icourse163.org request.
    /// </summary>
    public class MoocRequest
    {
        private readonly string     _courseId;  // identifier of the course.
        private readonly string     _sessionId; // Session id.
        private readonly HttpClient _client;    // HTTP Client.
        private readonly string     _tid;       // ?tid=xxxx

        private const string COURSE_URL = "https://www.icourse163.org/course/";
        private const string LEARN_URL  = "https://www.icourse163.org/learn/";

        /// <summary>
        /// Initializes a new instance of the Mooc request class with cookies and the specified url.
        /// </summary>
        /// <param name="cookies">cookies of icourse163.org.</param>
        /// <param name="courseUrl">url of the course.</param>
        public MoocRequest(IReadOnlyCollection<CookieModel> cookies, string courseUrl)
        {
            if (courseUrl.Contains("tid="))
            {
                var urlWithQuery = courseUrl;

                if (courseUrl.Contains("#"))
                {
                    urlWithQuery = courseUrl.Substring(0, courseUrl.IndexOf('#'));
                }

                var questionIndex = urlWithQuery.IndexOf('?');
                var query         = urlWithQuery.Substring(questionIndex + 1, urlWithQuery.Length - questionIndex - 1);
                var tidQuery      = query.Split('&').FirstOrDefault(s => s.Contains("tid"));

                if (tidQuery != null)
                {
                    _tid = tidQuery.Remove(0, tidQuery.IndexOf('=') + 1);
                }
            }
            else
            {
                _tid = string.Empty;
            }

            _courseId = GetCourseId(courseUrl);

            _sessionId = cookies.FirstOrDefault(c => c.Name == "NTESSTUDYSI")?.Value ?? string.Empty;

            var cookieContainer = new CookieContainer(); // cookies of icourse163.org.

            foreach (var cookie in cookies) // add cookies for HTTP client.
            {
                cookieContainer.Add(new Cookie(cookie.Name, cookie.Value)
                {
                    Domain   = cookie.Host,
                    HttpOnly = cookie.IsHttpOnly ?? false,
                    Secure   = cookie.IsSecure   ?? false
                });
            }

            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                AllowAutoRedirect      = false,
                UseCookies             = true,
                CookieContainer        = cookieContainer
            };

            _client = new HttpClient(handler) {Timeout = TimeSpan.MaxValue};

            _client.DefaultRequestHeaders.Add(
                "user-agent",
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.135 Safari/537.36"
            );
        }

        /// <summary>
        /// Get course id from url.
        /// </summary>
        /// <param name="url">URL of course.</param>
        /// <returns>course id.</returns>
        public static string GetCourseId(string url)
        {
            /**
             * There are two types of URL:
             *
             * 1. https://www.icourse163.org/course/xxxxxx
             * 2. https://www.icourse163.org/learn/xxxx?tid=xxxx
             */
            var localPath = new Uri(url).LocalPath;

            if (localPath.Contains("course"))
            {
                return localPath.Replace("course", "").Replace("/", "");
            }

            if (localPath.Contains("learn"))
            {
                return localPath.Replace("learn", "").Replace("/", "");
            }

            return string.Empty;
        }

        /// <summary>
        /// Get termId value.
        /// </summary>
        /// <returns>Term id.</returns>
        public async Task<string> GetTermIdAsync()
        {
            if (string.IsNullOrEmpty(_tid))
            {
                var response     = await _client.GetStringAsync($@"{COURSE_URL}{_courseId}");
                var htmlDocument = new HtmlDocument();

                htmlDocument.LoadHtml(response);

                var node = htmlDocument.DocumentNode
                                       .SelectNodes("//div")
                                       .FirstOrDefault(n => n?.Attributes?["data-action"]?.Value == "点击详情tab");

                return node != null ? node.Attributes["data-label"].Value : string.Empty;
            }

            return _tid;
        }

        /// <summary>
        /// Get /dwr/call/plaincall/CourseBean.getMocTermDto.dwr
        /// </summary>
        /// <param name="termId">Term Id.</param>
        /// <returns>The index of course JavaScript Code.</returns>
        public async Task<string> GetMocTermJavaScriptCodeAsync(string termId)
        {
            const string url = "https://www.icourse163.org/dwr/call/plaincall/CourseBean.getMocTermDto.dwr";

            var request     = new HttpRequestMessage(HttpMethod.Post, url);
            var bodyBuilder = new StringBuilder();

            bodyBuilder.AppendLine(@"callCount=1");
            bodyBuilder.AppendLine(@"scriptSessionId=${scriptSessionId}190");
            bodyBuilder.AppendLine($@"httpSessionId={_sessionId}");
            bodyBuilder.AppendLine(@"c0-scriptName=CourseBean");
            bodyBuilder.AppendLine(@"c0-methodName=getMocTermDto");
            bodyBuilder.AppendLine(@"c0-id=0");
            bodyBuilder.AppendLine($@"c0-param0=number:{termId}");
            bodyBuilder.AppendLine(@"c0-param1=number:0");
            bodyBuilder.AppendLine(@"c0-param2=boolean:true");
            bodyBuilder.AppendLine($@"batchId={new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()}");

            request.Content          = new StringContent(bodyBuilder.ToString());
            request.Headers.Referrer = new Uri($@"{LEARN_URL}{_courseId}");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Origin", "https://www.icourse163.org");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }

        /// <summary>
        /// Get /dwr/call/plaincall/CourseBean.getLessonUnitLearnVo.dwr
        /// </summary>
        /// <param name="unitId">Unit Id.</param>
        /// <param name="contentId">Content Id.</param>
        /// <param name="termId">Term Id.</param>
        /// <param name="contentType">Content Type.</param>
        /// <returns>Unit JavaScript code.</returns>
        public async Task<string> GetUnitJavaScriptCodeAsync(long? unitId, long? contentId, long? termId,
                                                             long? contentType)
        {
            const string url = "https://www.icourse163.org/dwr/call/plaincall/CourseBean.getLessonUnitLearnVo.dwr";

            var request     = new HttpRequestMessage(HttpMethod.Post, url);
            var bodyBuilder = new StringBuilder();

            bodyBuilder.AppendLine(@"callCount=1");
            bodyBuilder.AppendLine(@"scriptSessionId=${scriptSessionId}190");
            bodyBuilder.AppendLine($@"httpSessionId={_sessionId}");
            bodyBuilder.AppendLine(@"c0-scriptName=CourseBean");
            bodyBuilder.AppendLine(@"c0-methodName=getLessonUnitLearnVo");
            bodyBuilder.AppendLine(@"c0-id=0");
            bodyBuilder.AppendLine($@"c0-param0=number:{contentId}");
            bodyBuilder.AppendLine($@"c0-param1=number:{contentType}");
            bodyBuilder.AppendLine(@"c0-param2=number:0");
            bodyBuilder.AppendLine($@"c0-param3=number:{unitId}");
            bodyBuilder.AppendLine($@"batchId={new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()}");

            request.Content          = new StringContent(bodyBuilder.ToString());
            request.Headers.Referrer = new Uri($@"{LEARN_URL}{_courseId}?tid={termId}");
            request.Headers.Add("Origin", "https://www.icourse163.org");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }

        /// <summary>
        /// Get /web/j/resourceRpcBean.getResourceToken.rpc
        /// </summary>
        /// <param name="unitId">Unit Id.</param>
        /// <param name="termId">Term Id.</param>
        /// <param name="contentType">Content type.</param>
        /// <returns>JSON of resource.</returns>
        public async Task<string> GetResourceTokenJsonAsync(string unitId, string termId, string contentType)
        {
            const string url = "https://www.icourse163.org/web/j/resourceRpcBean.getResourceToken.rpc";

            var request = new HttpRequestMessage(HttpMethod.Post, $@"{url}?csrfKey={_sessionId}")
            {
                Content = new StringContent($@"bizId={unitId}&bizType=1&contentType={contentType}")
            };

            request.Headers.Referrer = new Uri($@"{LEARN_URL}{_courseId}?tid={termId}");

            request.Headers.Add("Origin", "https://www.icourse163.org");
            request.Headers.Add("DNT", "1");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }

        /// <summary>
        /// Get ds/api/v1/vod/video
        /// </summary>
        /// <param name="videoId">Video id.</param>
        /// <param name="signature">Signature</param>
        /// <returns>Video JSON</returns>
        public async Task<string> GetVideoJsonAsync(string videoId, string signature)
        {
            const string url = @"https://vod.study.163.com/eds/api/v1/vod/video";

            var request = new HttpRequestMessage(
                HttpMethod.Get, $"{url}?videoId={videoId}&signature={signature}&clientType=1"
            );

            request.Headers.Referrer = new Uri($@"{LEARN_URL}{_courseId}");
            request.Headers.Add("Origin", "https://www.icourse163.org");

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }

        /// <summary>
        /// Download ts file of m3u8 play list from url.
        /// </summary>
        /// <param name="videoUrl">url of ts file in m3u8 play list.</param>
        /// <returns>content of m3u8 play list.</returns>
        public async Task<byte[]> DownloadM3U8TSAsync(Uri tsUrl)
        {
            var request  = new HttpRequestMessage(HttpMethod.Get, tsUrl);
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            return null;
        }

        /// <summary>
        /// download video file from url.
        /// </summary>
        /// <returns></returns>
        public async Task<byte[]> DownloadVideoAsync(string url)
        {
            var request  = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            return null;
        }

        /// <summary>
        /// Download m3u8 play list from url.
        /// </summary>
        /// <param name="videoUrl">url of m3u8 file.</param>
        /// <returns>content of m3u8 play list.</returns>
        public async Task<string> DownloadM3U8ListAsync(Uri videoUrl)
        {
            var request  = new HttpRequestMessage(HttpMethod.Get, videoUrl);
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }

        /// <summary>
        /// Download the document from url.
        /// </summary>
        /// <param name="documentUrl">document's url.</param>
        /// <returns>bytes of document with url.</returns>
        public async Task<byte[]> DownloadDocumentAsync(string documentUrl)
        {
            var request  = new HttpRequestMessage(HttpMethod.Get, documentUrl);
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            return null;
        }

        /// <summary>
        /// Download the attachment from given url.
        /// </summary>
        /// <param name="attachmentUrl">attachment's url.</param>
        /// <returns>bytes of attachment with url.</returns>
        public async Task<byte[]> DownloadAttachmentAsync(string attachmentUrl)
        {
            var request  = new HttpRequestMessage(HttpMethod.Get, attachmentUrl);
            var response = await _client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Redirect && response.Headers.Location != null)
            {
                return await _client.GetByteArrayAsync(response.Headers.Location); // Redirect
            }

            return null;
        }

        /// <summary>
        /// Download the subtitle from the given url.
        /// </summary>
        /// <param name="subtitleUrl">subtitle's url.</param>
        /// <returns>bytes of subtitle with url.</returns>
        public async Task<byte[]> DownloadSubtitleAsync(string subtitleUrl)
        {
            var request  = new HttpRequestMessage(HttpMethod.Get, subtitleUrl);
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            return null;
        }
    }
}