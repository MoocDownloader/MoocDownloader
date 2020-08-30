using MoocDownloader.App.M3U8.Core;
using MoocDownloader.App.M3U8.Utilities;

namespace MoocDownloader.App.M3U8.AttributeReaders
{
    internal abstract class AttributeReader : IAttributeReader
    {
        protected abstract bool CanRead(string key);

        protected virtual bool ShouldTerminate()
        {
            return false;
        }

        protected abstract void Write(M3UFileInfo fileInfo, string value, LineReader reader);

        public bool Read(LineReader reader, M3UFileInfo fileInfo)
        {
            var text = reader.Current?.Trim();
            if (string.IsNullOrEmpty(text) || '#' != text[0])
                return false;

            var keyValuePair = KV.Parse(text, ':').Value;
            if (!CanRead(keyValuePair.Key))
                return false;
            if (ShouldTerminate())
                return true;

            Write(fileInfo, keyValuePair.Value, reader);
            return false;
        }
    }
}