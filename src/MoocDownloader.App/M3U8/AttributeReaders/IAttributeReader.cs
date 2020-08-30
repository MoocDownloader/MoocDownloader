using MoocDownloader.App.M3U8.Core;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal interface IAttributeReader
    {
        bool Read(LineReader reader, M3UFileInfo fileInfo);
    }
}