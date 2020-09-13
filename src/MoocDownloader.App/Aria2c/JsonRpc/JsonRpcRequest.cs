namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Represents a request to a server made using the Json RPC v2.0 specification.
    /// </summary>
    /// <typeparam name="T">The type to use for the <seealso cref="Arguments"/> property. Normally either a Dictionary&lt;string, object&gt; for named arguments or an object array for positional arguments.</typeparam>
    public class JsonRpcRequest<T>
    {

        /// <summary>
        /// Gets or sets the Json RPC version being used. Defaults to <seealso cref="JsonRpcVersions.Version2"/>.
        /// </summary>
        /// /// <seealso cref="JsonRpcVersions"/>
        /// <seealso cref="JsonRpcVersions.Version2"/>
        /// <value>The version.</value>
        [Newtonsoft.Json.JsonProperty("jsonrpc")]
        public string Version { get; set; } = "2.0";

        /// <summary>
        /// Gets or sets the unique id of the request, used to correlate responses.
        /// </summary>
        /// <remarks>
        /// <para>By default, a unique integer value is provided (in a thread-safe manner).</para>
        /// </remarks>
        /// <value>The identifier.</value>
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; } = JsonRpcRequestIdGenerator.GenerateUniqueId();

        /// <summary>
        /// Gets or sets the name of the remote method to be called.
        /// </summary>
        /// <value>The name of the method.</value>
        [Newtonsoft.Json.JsonProperty("method")]

        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets a dictionary of arguments to pass to the remote method.
        /// </summary>
        /// <value>The arguments.</value>
        [Newtonsoft.Json.JsonProperty("params")]
        public T Arguments { get; set; }

    }
}