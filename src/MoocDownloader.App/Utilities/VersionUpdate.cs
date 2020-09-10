using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MoocDownloader.App.Models;
using Newtonsoft.Json;

namespace MoocDownloader.App.Utilities
{
    /// <summary>
    /// Version upgrade for downloader.
    /// </summary>
    public class VersionUpdate
    {
        private readonly HttpClient _client; // HTTP Client.

        /// <summary>
        /// Link for update.
        /// </summary>
        private const string UPDATE_URL =
#if DEBUG
            @"https://yufan.io/xinqian/mooc-downloader-update/raw/branch/master/test-mooc-version.json";
#else
            @"https://yufan.io/xinqian/mooc-downloader-update/raw/branch/master/mooc-version.json";
#endif

        public VersionUpdate()
        {
            _client = new HttpClient(new HttpClientHandler
            {
                AllowAutoRedirect      = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            });
        }

        /// <summary>
        /// Check if there is a new version.
        /// </summary>
        /// <returns>new version info.</returns>
        public async Task<NewVersion> GetNewVersionAsync()
        {
            try
            {
                var versionText = await _client.GetStringAsync(UPDATE_URL);

                if (string.IsNullOrEmpty(versionText))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<NewVersion>(versionText);
            }
            catch // exception soft handling
            {
                return null;
            }
        }
    }
}