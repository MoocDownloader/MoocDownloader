using HtmlAgilityPack;
using MoocDownloader.App.Models;
using MoocDownloader.App.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Cookie = System.Net.Cookie;

namespace MoocDownloader.App.Mooc
{
    /// <summary>
    /// icourse163.org request.
    /// </summary>
    public class MoocRequest
    {
        private readonly HttpConsumer     _consumer;  // HTTP consumer.
        private readonly string           _courseId;  // identifier of the course.
        private readonly CookieCollection _cookies;   // cookies of icourse163.org.
        private readonly string           _sessionId; // Session id.

        private const string COURSE_URL = "https://www.icourse163.org/course/";
        private const string LEARN_URL  = "https://www.icourse163.org/learn/";

        /// <summary>
        /// Initializes a new instance of the Mooc request class with cookies and the specified url.
        /// </summary>
        /// <param name="cookies">cookies of icourse163.org.</param>
        /// <param name="courseUrl">url of the course.</param>
        public MoocRequest(IReadOnlyCollection<CookieModel> cookies, string courseUrl)
        {
            _consumer = HttpConsumer.Create();
            _courseId = GetCourseId(courseUrl);
            _cookies  = new CookieCollection();

            foreach (var cookie in cookies)
            {
                _cookies.Add(new Cookie(cookie.Name, cookie.Value));
            }

            _sessionId = cookies.FirstOrDefault(c => c.Name == "NTESSTUDYSI")?.Value ?? string.Empty;
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
        public string GetTermId()
        {
            var request  = HttpRequest.Create().SetUrl($@"{COURSE_URL}{_courseId}").SetMethod(HttpMethod.GET);
            var response = _consumer.Send(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var html = new HtmlDocument();

                html.LoadHtml(response.Content);

                var node = html.DocumentNode
                               .SelectNodes("//div")
                               .FirstOrDefault(n => n?.Attributes?["data-action"]?.Value == "点击详情tab");

                if (node != null)
                {
                    return node.Attributes["data-label"].Value;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get /dwr/call/plaincall/CourseBean.getMocTermDto.dwr
        /// </summary>
        /// <param name="termId">Term Id.</param>
        /// <returns>The index of course JavaScript Code.</returns>
        public string GetMocTermJavaScriptCode(string termId)
        {
            const string url = "https://www.icourse163.org/dwr/call/plaincall/CourseBean.getMocTermDto.dwr";

            var request     = HttpRequest.Create().SetUrl(url).SetMethod(HttpMethod.POST);
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

            request.PostBodyType     = PostBodyType.String;
            request.PostBodyText     = bodyBuilder.ToString();
            request.Referer          = $@"{LEARN_URL}{_courseId}";
            request.ContentType      = "text/plain";
            request.CookieCollection = _cookies;

            request.Header["DNT"]    = "1";
            request.Header["Origin"] = "https://www.icourse163.org";

            var response = _consumer.Send(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get /dwr/call/plaincall/CourseBean.getLessonUnitLearnVo.dwr
        /// </summary>
        /// <param name="contentId">Content Id.</param>
        /// <param name="unitId">Unit Id.</param>
        /// <returns>Lesson JavaScript code.</returns>
        public string GetLessonJavaScriptCode(string contentId, string unitId)
        {
            const string url = "https://www.icourse163.org/dwr/call/plaincall/CourseBean.getLessonUnitLearnVo.dwr";

            var request     = HttpRequest.Create().SetUrl(url).SetMethod(HttpMethod.POST);
            var bodyBuilder = new StringBuilder();

            bodyBuilder.AppendLine(@"callCount=1");
            bodyBuilder.AppendLine(@"scriptSessionId=${scriptSessionId}190");
            bodyBuilder.AppendLine($@"httpSessionId={_sessionId}");
            bodyBuilder.AppendLine(@"c0-scriptName=CourseBean");
            bodyBuilder.AppendLine(@"c0-methodName=getLessonUnitLearnVo");
            bodyBuilder.AppendLine(@"c0-id=0");
            bodyBuilder.AppendLine($@"c0-param0=number:{contentId}");
            bodyBuilder.AppendLine(@"c0-param1=number:1");
            bodyBuilder.AppendLine(@"c0-param2=number:0");
            bodyBuilder.AppendLine($@"c0-param3=number:{unitId}");
            bodyBuilder.AppendLine($@"batchId={new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()}");

            request.PostBodyType     = PostBodyType.String;
            request.PostBodyText     = bodyBuilder.ToString();
            request.Referer          = $@"{LEARN_URL}{_courseId}";
            request.ContentType      = "text/plain";
            request.CookieCollection = _cookies;

            request.Header["Origin"] = "https://www.icourse163.org";

            var response = _consumer.Send(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get /web/j/resourceRpcBean.getResourceToken.rpc
        /// </summary>
        /// <param name="unitId">Unit Id.</param>
        /// <param name="contentType">Content type.</param>
        /// <returns>JSON of resource.</returns>
        public string GetResourceTokenJSON(string unitId, string contentType)
        {
            const string url = "https://www.icourse163.org/web/j/resourceRpcBean.getResourceToken.rpc";

            var request = HttpRequest.Create().SetUrl(url).SetMethod(HttpMethod.POST)
                                     .SetQueryParameter("csrfKey", _sessionId);

            request.PostBodyType     = PostBodyType.String;
            request.PostBodyText     = $@"bizId={unitId}&bizType=1&contentType={contentType}";
            request.Referer          = $@"{LEARN_URL}{_courseId}";
            request.ContentType      = "text/plain";
            request.CookieCollection = _cookies;

            request.Header["Origin"] = "https://www.icourse163.org";

            var response = _consumer.Send(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }

            return string.Empty;
        }

        /// <summary>
        /// Get ds/api/v1/vod/video
        /// </summary>
        /// <param name="videoId">Video id.</param>
        /// <param name="signature">Signature</param>
        /// <returns>Video JSON</returns>
        public string GetVideoJSON(string videoId, string signature)
        {
            const string url = @"https://vod.study.163.com/eds/api/v1/vod/video";

            var request = HttpRequest.Create().SetUrl(url).SetMethod(HttpMethod.GET)
                                     .SetQueryParameter("videoId", videoId)
                                     .SetQueryParameter("signature", signature)
                                     .SetQueryParameter("clientType", "1");

            request.Referer          = $@"{LEARN_URL}{_courseId}";
            request.ContentType      = "application/x-www-form-urlencoded";
            request.CookieCollection = _cookies;

            request.Header["Origin"] = "https://www.icourse163.org";

            var response = _consumer.Send(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }

            return string.Empty;
        }
    }
}