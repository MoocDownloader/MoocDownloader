using MoocDownloader.App.M3U8.Core;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal class EndListAttributeReader : AttributeReader
    {
        protected override bool CanRead(string key)
        {
            return string.Equals("#EXT-X-ENDLIST", key);
        }

        protected override bool ShouldTerminate()
        {
            return true;
        }

        protected override void Write(M3UFileInfo fileInfo, string value, LineReader reader)
        {
        }
    }
}