using System;
using System.Net.Http;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// An HTTP transport for Json RPC requests, using an <see cref="HttpClient"/> to make requests.
    /// </summary>
    /// <remarks>
    /// <para>You can inject your own <see cref="HttpClient"/> instance via the constructors to control the HTTP pipeline and add features such as authorisation etc.</para>
    /// <para>If no <see cref="HttpClient"/> is injected the system creates a new instance with GZIP and Deflate compression support enabled.</para>
    /// </remarks>
    public class JsonRpcHttpTransport : HttpClientTransport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcHttpTransport"/> class.
        /// </summary>
        /// <remarks>
        /// <para>Creates an instance using a new <see cref="HttpClient"/> instance with compression enabled and <see cref="System.Text.UTF8Encoding"/> encoding.</para>
        /// </remarks>
        /// <param name="serviceAddress">The service address.</param>
        public JsonRpcHttpTransport(Uri serviceAddress) :
            this
            (
                serviceAddress,
                CreateDefaultJsonRpcHttpClient()
            )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcHttpTransport"/> class.
        /// </summary>
        /// <remarks>
        /// <para>Creates an instance using a new <see cref="HttpClient"/> instance with compression enabled and <see cref="System.Text.UTF8Encoding"/> encoding.</para>
        /// </remarks>
        /// <param name="serviceAddress">The service address.</param>
        /// <param name="httpClient">An <see cref="System.Net.Http.HttpClient"/> to use when making HTTP requests.</param>
        public JsonRpcHttpTransport(Uri serviceAddress, HttpClient httpClient) :
            base
            (
                serviceAddress, JsonRpcMediaTypes.ApplicationJson,
                System.Text.Encoding.UTF8.WebName.ToLower(),
                httpClient
            )
        {
        }

        private static HttpClient CreateDefaultJsonRpcHttpClient()
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression =
                    System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip;

            var client = new HttpClient(handler);
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(JsonRpcMediaTypes.ApplicationJson));
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(JsonRpcMediaTypes
                   .ApplicationJsonRequest));
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(JsonRpcMediaTypes.ApplicationJson));

            return client;
        }
    }
}