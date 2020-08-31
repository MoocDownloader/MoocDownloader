namespace MoocDownloader.App.M3U8.Core
{
    internal static class M3UAttributes
    {
        public const char TagIdentifier = '#';
        public const char TagSeparator = ':';
        public const char PairSeparator = '=';
        public const char AttributeSeparator = ',';
        public const string Header = "#EXTM3U";
        public const string Key = "#EXT-X-KEY";
        public const string Version = "#EXT-X-VERSION";
        public const string AllowCache = "#EXT-X-ALLOW-CACHE";
        public const string PlaylistType = "#EXT-X-PLAYLIST-TYPE";
        public const string MediaSequence = "#EXT-X-MEDIA-SEQUENCE";
        public const string TargetDuration = "#EXT-X-TARGETDURATION";
        public const string ProgramDateTime = "#EXT-X-PROGRAM-DATE-TIME";
        public const string Inf = "#EXTINF";
        public const string StreamInf = "#EXT-X-STREAM-INF";
        public const string EndList = "#EXT-X-ENDLIST";

        public static class Predicates
        {
            public const string Yes = "YES";
            public const string No = "NO";
        }

        public static class EncryptionMethods
        {
            public const string None = "NONE";
            public const string AES128 = "AES-128";
        }

        public static class PlaylistTypes
        {
            public const string Event = "EVENT";
            public const string VideoOnDemand = "VOD";
        }

        public static class KeyAttributes
        {
            public const string Method = "METHOD";
            public const string Uri = "URI";
            public const string IV = "IV";
        }

        public static class StreamInfAttributes
        {
            public const string Codecs = "CODECS";
            public const string Bandwidth = "BANDWIDTH";
            public const string ProgramId = "PROGRAM-ID";
            public const string Resolution = "RESOLUTION";
        }
    }
}