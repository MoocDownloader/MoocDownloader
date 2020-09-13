namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// A generalized response from an RPC call.
    /// </summary>
    /// <typeparam name="T">The type of value wrapped by the response envelope.</typeparam>
    public class RpcResponse<T>
    {
        /// <summary>
        /// Gets or sets the result of the method call. Null if an error occurred, or if the method does not return a value.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; set; }

        /// <summary>
        /// Gets or sets the error that occurred. Null if no error occurred.
        /// </summary>
        /// <value>The error.</value>
        public RpcError Error { get; set; }
    }
}