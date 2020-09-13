namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// An interface for components that can serialize <see cref="RpcRequest"/> objects and deserialize the responses from RPC servers.
    /// </summary>
    /// <remarks>
    /// <para>Components implementing this interface are responsible for formatting requests and responses before they are sent over the network. This is the layer
    /// where a particular RPC format is implemented, such as Json vs XML RPC. The serializer knows nothing about how the data will be sent, only how to format it.
    /// </para>
    /// </remarks>
    public interface IRpcSerializer
    {
        /// <summary>
        /// Serializes the specified <see cref="RpcRequest"/> to a stream.
        /// </summary>
        /// <param name="request">The <see cref="RpcRequest"/> to serialize.</param>
        /// <param name="outputStream">The stream to write the serialized output to.</param>
        void Serialize(RpcRequest request, System.IO.Stream outputStream);

        /// <summary>
        /// Deserializes the specified <see cref="RpcResponse{T}"/> from a stream.
        /// </summary>
        /// <typeparam name="T">The type of value wrapped by the response envelope to be deserialized.</typeparam>
        /// <param name="serializedData">The stream to read the serialized content from.</param>
        /// <returns>A <see cref="RpcResponse{T}"/> containing the deserialized content.</returns>
        RpcResponse<T> Deserialize<T>(System.IO.Stream serializedData);
    }
}