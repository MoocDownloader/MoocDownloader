using System;
using System.Text;

namespace MoocDownloader.App.M3U8
{
    public sealed class Configuration
    {
        public Uri BaseUri { get; set; }

        public int RequestTimeout { get; set; } = 5000;

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public string UserAgent { get; set; } = string.Join(
            " ", "Mozilla/5.0",
            $"({Environment.OSVersion.VersionString}; {Environment.OSVersion.Platform})",
            "AppleWebKit/605.1.15",
            "(KHTML, like Gecko) Version/12.0.1", "Safari/605.1.15"
        );

        public static Configuration Default { get; } = new Configuration();

        private Configuration()
        {
        }
    }
}