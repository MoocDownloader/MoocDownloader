namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Provides constants for HTTP content media types for json RPC requests.
    /// </summary>
    public static class JsonRpcMediaTypes
    {
        /// <summary>
        /// The preferred media type string for http content containing a json rpc request or response.
        /// </summary>
        public const string ApplicationJsonRpc = "application/json-rpc";

        /// <summary>
        /// A commonly used, but less than ideal media type for json rpc content. Use this only if required by your specific server(s).
        /// </summary>
        public const string ApplicationJson = "application/json";

        /// <summary>
        /// An allowed media type for json rpc content, preferred over <see cref="ApplicationJson"/>, but less ideal than the recommended <see cref="ApplicationJsonRpc"/>
        /// </summary>
        public const string ApplicationJsonRequest = "application/jsonrequest";
    }
}