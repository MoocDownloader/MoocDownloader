using MoocDownloader.App.M3U8.Core;
using MoocDownloader.App.M3U8.Utilities;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal class AllowCacheAttributeReader : AttributeReader
    {
        protected override bool CanRead(string key)
        {
            return string.Equals("#EXT-X-ALLOW-CACHE", key);
        }

        protected override void Write(M3UFileInfo fileInfo, string value, LineReader reader)
        {
            fileInfo.AllowCache = To.Value<bool>(value);
        }
    }
}