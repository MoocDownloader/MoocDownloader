using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// An <see cref="IRpcTransport"/> implementation that uses <see cref="System.Net.Http.HttpClient"/> to transmit RPC messages.
    /// </summary>
    public class HttpClientTransport : IRpcTransport
    {
        private readonly HttpClient _Client;
        private readonly Uri _ServerAddress;
        private readonly string _ContentType;
        private readonly string _CharSet;

        /// <summary>
        /// Creates a new <see cref="HttpClientTransport"/> instance using the specified service address and a new instance of <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="serviceAddress">The url of the RPC service to call.</param>
        /// <param name="contentType">Optional. The media type for the Content-Type HTTP header for requests sent via this transport.</param>
        /// <param name="charSet">Optional. The character set/text encoding that content sent via this transport is compatible with.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="serviceAddress"/> is null.</exception>
        public HttpClientTransport(Uri serviceAddress, string contentType, string charSet) : this(serviceAddress,
            contentType, charSet, CreateDefaultHttpClient())
        {
        }

        /// <summary>
        /// Creates a new <see cref="HttpClientTransport"/> instance using the specified <see cref="System.Net.Http.HttpClient"/> and service address.
        /// </summary>
        /// <param name="serviceAddress">The url of the RPC service to call.</param>
        /// <param name="contentType">Optional. The media type for the Content-Type HTTP header for requests sent via this transport.</param>
        /// <param name="charSet">Optional. The character set/text encoding that content sent via this transport is compatible with.</param>
        /// <param name="client">An instance of <see cref="HttpClient"/> to be used when making RPC requests.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="serviceAddress"/> or <paramref name="client"/> is null.</exception>
        public HttpClientTransport(Uri serviceAddress, string contentType, string charSet, HttpClient client)
        {
            client.GuardNull(nameof(client));
            serviceAddress.GuardNull(nameof(serviceAddress));

            _Client = client;
            _ContentType = contentType;
            _CharSet = charSet;
            _ServerAddress = serviceAddress;
        }

        /// <summary>
        /// Sends a request to the remote server.
        /// </summary>
        /// <param name="requestContent">The content of the request to send as a stream.</param>
        /// <returns>The response from the server, as a stream.</returns>
        public async Task<Stream> SendRequest(Stream requestContent)
        {
            HttpContent content = null;
            try
            {
                content = new StreamContent(requestContent);
                if (!String.IsNullOrEmpty(_ContentType))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(_ContentType);
                    content.Headers.ContentType.CharSet = _CharSet;
                }
            }
            catch
            {
                content?.Dispose();
                throw;
            }

            var response = await _Client.PostAsync(_ServerAddress, content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        private static HttpClient CreateDefaultHttpClient()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression =
                    System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip;

            return new HttpClient(handler);
        }
    }
}