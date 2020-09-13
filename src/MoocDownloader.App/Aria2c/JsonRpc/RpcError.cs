namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// A generalized representation of an error response from an RPC call.
    /// </summary>
#if SUPPORTS_SERIALIZATION
	[System.Serializable]
#endif
    public class RpcError
    {
        /// <summary>
        /// Gets or sets a numeric error code.
        /// </summary>
        /// <value>The code.</value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets any server/method specific data provided about the error.
        /// </summary>
        /// <value>The data.</value>
        public object Data { get; set; }
    }
}