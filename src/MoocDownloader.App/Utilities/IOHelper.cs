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
            var fixedName    = new string(name.Where(m => !invalidChars.Contains(m)).ToArray());

            return fixedName.Replace(".", "").Trim();
        }
    }
}