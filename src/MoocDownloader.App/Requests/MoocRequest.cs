using HtmlAgilityPack;
using MoocDownloader.App.Models;
using MoocDownloader.App.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.JScript.Vsa;
using Cookie = System.Net.Cookie;

namespace MoocDownloader.App.Requests
{
    /// <summary>
    /// icourse163.org request.
    /// </summary>
    public class MoocRequest
    {
        private static   VsaEngine    _engine = VsaEngine.CreateEngine();
        private readonly HttpConsumer _consumer;

        private const string COURSE_URL = "https://www.icourse163.org/course/";
        private const string LEARN_URL  = "https://www.icourse163.org/learn/";

        public MoocRequest()
        {
            _consumer = HttpConsumer.Create();
        }

        /// <summary>
        /// Get termId value.
        /// </summary>
        /// <param name="url">URL of the target course.</param>
        /// <returns></returns>
        public string GetTermId(string url)
        {
            /**
             * There are two types of URL:
             *
             * 1. https://www.icourse163.org/course/xxxxxx
             * 2. https://www.icourse163.org/learn/xxxx?tid=xxxx
             */
            var localPath = new Uri(url).LocalPath;
            var courseId  = string.Empty;

            if (localPath.Contains("course"))
            {
                courseId = localPath.Replace("course", "").Replace("/", "");
            }
            else if (localPath.Contains("learn"))
            {
                courseId = localPath.Replace("learn", "").Replace("/", "");
            }

            var request  = HttpRequest.Create().SetUrl($@"{COURSE_URL}{courseId}").SetMethod(HttpMethod.GET);
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
        /// <param name="courseId">Course Id.</param>
        /// <param name="cookies">Cookies</param>
        /// <returns>The index of course JavaScript Code.</returns>
        public string GetMocTerm(string termId, string courseId, List<CookieModel> cookies)
        {
            const string url = "https://www.icourse163.org/dwr/call/plaincall/CourseBean.getMocTermDto.dwr";

            var request     = HttpRequest.Create().SetUrl(url).SetMethod(HttpMethod.POST);
            var bodyBuilder = new StringBuilder();
            var sessionId   = cookies.FirstOrDefault(c => c.Name == "NTESSTUDYSI")?.Value;

            bodyBuilder.AppendLine(@"callCount=1");
            bodyBuilder.AppendLine(@"scriptSessionId=${scriptSessionId}190");
            bodyBuilder.AppendLine($@"httpSessionId={sessionId}");
            bodyBuilder.AppendLine(@"c0-scriptName=CourseBean");
            bodyBuilder.AppendLine(@"c0-methodName=getMocTermDto");
            bodyBuilder.AppendLine(@"c0-id=0");
            bodyBuilder.AppendLine($@"c0-param0=number:{termId}");
            bodyBuilder.AppendLine(@"c0-param1=number:0");
            bodyBuilder.AppendLine(@"c0-param2=boolean:true");
            bodyBuilder.AppendLine($@"batchId={new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds()}");

            request.PostBodyType = PostBodyType.String;
            request.PostBodyText = bodyBuilder.ToString();
            request.Referer      = $@"{LEARN_URL}{courseId}";
            request.ContentType  = "text/plain";

            foreach (var cookie in cookies)
            {
                request.CookieCollection.Add(new Cookie(cookie.Name, cookie.Value));
            }

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
        /// Eval JavaScript code.
        /// </summary>
        /// <param name="code">JavaScript code.</param>
        /// <returns>JavaScript code return result.</returns>
        private static object EvalJavaScript(string code)
        {
            try
            {
                return Microsoft.JScript.Eval.JScriptEvaluate(code, _engine);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}