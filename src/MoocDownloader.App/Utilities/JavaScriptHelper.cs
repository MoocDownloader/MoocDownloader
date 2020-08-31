using MsieJavaScriptEngine;
using System;

namespace MoocDownloader.App.Utilities
{
    /// <summary>
    /// JavaScript helper class.
    /// </summary>
    public class JavaScriptHelper
    {
        /// <summary>
        /// Eval JavaScript code.
        /// </summary>
        /// <param name="code">JavaScript code.</param>
        /// <param name="variableName">Variable name.</param>
        /// <returns>JavaScript code return result.</returns>
        public static object EvaluateJavaScriptCode(string code, string variableName)
        {
            try
            {
                using var engine = new MsieJsEngine();
                engine.Execute(code);

                return engine.GetVariableValue(variableName);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}