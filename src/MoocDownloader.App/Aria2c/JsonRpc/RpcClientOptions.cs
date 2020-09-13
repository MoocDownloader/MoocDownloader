namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Represents options, configuration data and injected dependencies for a <see cref="RpcClient"/>.
    /// </summary>
    public class RpcClientOptions
    {
        /// <summary>
        /// Required. Gets or sets the <see cref="IRpcSerializer"/> implementation to use when serializing requests and deserializing responses.
        /// </summary>
        /// <value>The transport.</value>
        public IRpcSerializer Serializer { get; set; }

        /// <summary>
        /// Required. Gets or sets the <see cref="IRpcTransport"/> implementation to use when sending requests.
        /// </summary>
        /// <value>The transport.</value>
        public IRpcTransport Transport { get; set; }
    }
}