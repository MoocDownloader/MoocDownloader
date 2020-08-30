using MoocDownloader.App.M3U8.Core;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal class PlaylistTypeAttributeReader : AttributeReader
    {
        protected override bool CanRead(string key)
        {
            return string.Equals("#EXT-X-PLAYLIST-TYPE", key);
        }

        protected override void Write(M3UFileInfo fileInfo, string value, LineReader reader)
        {
            fileInfo.PlaylistType = value;
        }
    }
}