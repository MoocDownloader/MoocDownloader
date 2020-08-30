using MoocDownloader.App.M3U8.Core;
using MoocDownloader.App.M3U8.Utilities;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal class VersionAttributeReader : AttributeReader
    {
        protected override bool CanRead(string key)
        {
            return string.Equals("#EXT-X-VERSION", key);
        }

        protected override void Write(M3UFileInfo fileInfo, string value, LineReader reader)
        {
            fileInfo.Version = To.Value<int>(value);
        }
    }
}