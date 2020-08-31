using MoocDownloader.App.M3U8.Core;
using MoocDownloader.App.M3U8.Utilities;
using System;
using System.Linq;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal class KeyAttributeReader : AttributeReader
    {
        protected override bool CanRead(string key)
        {
            return string.Equals("#EXT-X-KEY", key);
        }

        protected override void Write(M3UFileInfo fileInfo, string value, LineReader reader)
        {
            var source =
                value.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                     .Select(e => KV.Parse(e, '=').Value);
            if (fileInfo.Key == null && source.Any())
                fileInfo.Key = new M3UKeyInfo();
            foreach (var keyValuePair in source)
            {
                var key = keyValuePair.Key;
                if (key != "URI")
                {
                    if (key != "IV")
                    {
                        if (key == "METHOD")
                            fileInfo.Key.Method = keyValuePair.Value;
                    }
                    else
                        fileInfo.Key.IV = keyValuePair.Value;
                }
                else
                {
                    fileInfo.Key.Uri = Uri.TryCreate(keyValuePair.Value, UriKind.Absolute, out var result)
                        ? result
                        : null;
                }
            }
        }
    }
}