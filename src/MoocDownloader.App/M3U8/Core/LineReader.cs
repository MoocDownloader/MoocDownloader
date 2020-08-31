using MoocDownloader.App.M3U8.Adapters;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace MoocDownloader.App.M3U8.Core
{
    internal sealed class LineReader : IEnumerator<string>
    {
        private readonly StreamReader reader;

        public IAdapter Adapter { get; }

        public string Current { get; private set; }

        object IEnumerator.Current => Current;

        public LineReader(IAdapter adapter)
        {
            reader = new StreamReader((Adapter = adapter).Connect());
        }

        public void Dispose()
        {
            reader.Dispose();
        }

        public bool MoveNext()
        {
            var reader = this.reader;
            var endOfStream = reader.EndOfStream;
            Current = endOfStream ? null : reader.ReadLine();
            return !endOfStream;
        }

        public void Reset()
        {
            var reader = this.reader;
            var baseStream = reader.BaseStream;
            if (baseStream.CanSeek)
                baseStream.Seek(0L, SeekOrigin.Begin);
            reader.DiscardBufferedData();
            Current = null;
        }
    }
}