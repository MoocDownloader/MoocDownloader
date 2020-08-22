using System.Text.RegularExpressions;

namespace MoocDownloader.App.Mooc
{
    /// <summary>
    /// Correct the JavaScript code to calculate the code execution result.
    /// </summary>
    public class MoocCodeCorrector
    {
        /// <summary>
        /// Fix code of `CourseBean.getMocTermDto.dwr` response.
        /// </summary>
        /// <param name="code">JavaScript code.</param>
        /// <returns>JavaScript code.</returns>
        public static string FixMoocTermCode(string code)
        {
            const string prefixRegex = @"dwr\.engine\._remoteHandleCallback\('[0-9]{13}','[0-9]',\{";
            const string replacement = @"var lessonJSON=JSON.stringify({";

            return Regex.Replace(code, prefixRegex, replacement);
        }
    }
}