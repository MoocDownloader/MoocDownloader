namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Contains a set of constants that represent official Json RPC error codes.
    /// </summary>
    public static class JsonRpcErrorCodes
    {
        /// <summary>
        /// An official error code representing an error on the server parsing the request.
        /// </summary>
        public const int ParseError = -32700;

        /// <summary>
        /// An official error code representing an invalid request.
        /// </summary>
        public const int InvalidRequest = -32600;

        /// <summary>
        /// An official error code representing a call to a non-existent method.
        /// </summary>
        public const int MethodNotFound = -32601;

        /// <summary>
        /// An official error code representing an invalid request.
        /// </summary>
        public const int InvalidParameters = -32602;

        /// <summary>
        /// An official error code representing an internal error on the server.
        /// </summary>
        public const int InternalError = -32603;
    }
}