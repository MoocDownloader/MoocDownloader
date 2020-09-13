namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// A generalized RPC request.
    /// </summary>
    public class RpcRequest
    {
        /// <summary>
        /// Gets or sets the name of the method to call.
        /// </summary>
        /// <value>A string containing the name of the method.</value>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the arguments for the request.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Tbe value must be one of the following
        /// <list type="Bullet">
        /// <item>Null (no arguments)</item>
        /// <item>A object array where each element is the value of the argument at the same ordinal position.</item>
        /// <item>An IEnumerable&lt;KeyValuePair&lt;string, object&gt;&gt; where each key value pair represents an argument name and value.</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <value>The arguments to pass to the remote method.</value>
        public object Arguments { get; set; }
    }
}