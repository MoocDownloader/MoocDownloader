using System;
using System.Collections.Generic;

namespace MoocDownloader.App.M3U8
{
    public class M3UFileInfo
    {
        public int? Version { get; set; }

        public int? MediaSequence { get; set; }

        public int? TargetDuration { get; set; }

        public bool? AllowCache { get; set; }

        public string PlaylistType { get; set; }

        public DateTime? ProgramDateTime { get; set; }

        public M3UKeyInfo Key { get; set; }

        public IList<M3UStreamInfo> Streams { get; set; }

        public IList<M3UMediaInfo> MediaFiles { get; set; }
    }
}