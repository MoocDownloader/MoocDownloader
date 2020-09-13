using System;
using System.Net.Http;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// An <see cref="IRpcClient"/> implementation for making Json RPC 2.0 calls.
    /// </summary>
    public class JsonRpcHttpClient : RpcClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcHttpClient"/> class.
        /// </summary>
        /// <param name="serviceAddress">The url of the Json RPC service this client accesses.</param>
        public JsonRpcHttpClient(Uri serviceAddress)
            : base
            (
                new RpcClientOptions()
                {
                    Serializer = new JsonRpcSerializer(),
                    Transport = new JsonRpcHttpTransport(serviceAddress)
                }
            )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcHttpClient"/> class.
        /// </summary>
        /// <param name="serviceAddress">The url of the Json RPC service this client accesses.</param>
        /// <param name="httpClient">An <see cref="System.Net.Http.HttpClient"/> to use when making HTTP requests.</param>
        public JsonRpcHttpClient(Uri serviceAddress, HttpClient httpClient)
            : base
            (
                new RpcClientOptions()
                {
                    Serializer = new JsonRpcSerializer(),
                    Transport = new JsonRpcHttpTransport(serviceAddress, httpClient)
                }
            )
        {
        }
    }
}