using System.Text.RegularExpressions;

namespace MoocDownloader.App.Mooc
{
    /// <summary>
    /// Correct the JavaScript code to calculate the code execution result.
    /// </summary>
    public class MoocCodeCorrector
    {
        /// <summary>
        /// Variable name of CourseBean.*.dwr.
        /// </summary>
        public const string COURSE_BEAN_NAME = "courseBeanJSON";

        /// <summary>
        /// Fix code of `CourseBean.*.dwr` response.
        /// </summary>
        /// <param name="code">JavaScript code.</param>
        /// <returns>JavaScript code.</returns>
        public static string FixCourseBeanCode(string code)
        {
            const string prefixRegex = @"dwr\.engine\._remoteHandleCallback\('[0-9]{13}','[0-9]',\{";

            var replacement = $@"var {COURSE_BEAN_NAME}=JSON.stringify({{";

            return Regex.Replace(code, prefixRegex, replacement);
        }
    }
}