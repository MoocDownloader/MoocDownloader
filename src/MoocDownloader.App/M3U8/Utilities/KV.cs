using System;
using System.Collections.Generic;

namespace MoocDownloader.App.M3U8.Utilities
{
    internal static class KV
    {
        public static KeyValuePair<string, string>? Parse(string text, char separator = ':')
        {
            if (string.IsNullOrEmpty(text))
                return new KeyValuePair<string, string>?();

            var strArray = text.Split(new[]
            {
                separator
            }, 2, StringSplitOptions.RemoveEmptyEntries);

            return new KeyValuePair<string, string>(strArray[0].Trim(),
                (strArray.Length > 1 ? strArray[1] : string.Empty).Trim(' ', '"'));
        }
    }
}