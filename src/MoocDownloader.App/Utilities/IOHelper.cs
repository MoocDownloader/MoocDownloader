using System.IO;
using System.Linq;

namespace MoocDownloader.App.Utilities
{
    /// <summary>
    /// IO helper.
    /// </summary>
    public class IOHelper
    {
        /// <summary>
        /// make a valid Windows filename from an arbitrary string.
        /// </summary>
        /// <param name="name">directory path or file name.</param>
        /// <returns>valid directory path or file name.</returns>
        public static string FixPath(string name)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            var fixedName    = new string(name.Replace("'", "_").Where(m => !invalidChars.Contains(m)).ToArray());

            var lastIndex = fixedName.LastIndexOf('.');

            if (lastIndex == -1)
            {
                return fixedName;
            }

            var prefix  = fixedName.Substring(0, lastIndex).Replace(".", "").Trim();
            var postfix = fixedName.Substring(lastIndex);

            return prefix + postfix;
        }
    }
}