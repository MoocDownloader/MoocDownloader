using System.Collections.Generic;

namespace MoocDownloader.App.M3U8
{
    public class M3U8File
    {
        private readonly List<StreamInfo> _streams = new List<StreamInfo>();

        public string Version { get; internal set; }

        public IReadOnlyList<StreamInfo> Streams
        {
            get => _streams;
        }

        internal void AddStream(StreamInfo stream)
        {
            _streams.Add(stream);
        }
    }
}