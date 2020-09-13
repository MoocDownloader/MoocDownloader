using System;
using System.Collections.Generic;
using System.IO;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Serializer for Json RPC 2.0 request and responses.
    /// </summary>
    /// <remarks>
    /// <para>Uses <see cref="System.Text.UTF8Encoding"/> for the text encoding if none or null specified.</para>
    /// </remarks>
    public class JsonRpcSerializer : IRpcSerializer
    {
        private static readonly object[] EmptyOrdinalArguments = new object[] { };

        private readonly System.Text.Encoding _TextEncoding;
        private readonly Newtonsoft.Json.JsonSerializer _Serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcSerializer"/> class.
        /// </summary>
        public JsonRpcSerializer() : this(new System.Text.UTF8Encoding(false))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcSerializer"/> class with a custom text encoding.
        /// </summary>
        /// <param name="textEncoding">The text encoding to use. If null, <see cref="System.Text.UTF8Encoding"/> is used.</param>
        public JsonRpcSerializer(System.Text.Encoding textEncoding) : this(textEncoding, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonRpcSerializer"/> class with a custom text encoding.
        /// </summary>
        /// <param name="textEncoding">The text encoding to use. If null, <see cref="System.Text.UTF8Encoding"/> is used.</param>
        /// <param name="settings">Settings for the Json.Net serializer, allows controlling things like date formats. Provide null for default settings.</param>
        public JsonRpcSerializer(System.Text.Encoding textEncoding, Newtonsoft.Json.JsonSerializerSettings settings)
        {
            _TextEncoding = textEncoding ?? System.Text.Encoding.UTF8;

            if (settings == null)
                _Serializer = Newtonsoft.Json.JsonSerializer.Create();
            else
                _Serializer = Newtonsoft.Json.JsonSerializer.Create(settings);
        }


        /// <summary>
        /// Deserializes a response from the server.
        /// </summary>
        /// <typeparam name="T">The type for the payment of the response.</typeparam>
        /// <param name="serializedData">A stream containing the serialized data from the serve.</param>
        /// <returns>A <see cref="RpcResponse{T}"/> where the result is the payload from the server.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2202:Do not dispose objects multiple times")]
        public RpcResponse<T> Deserialize<T>(Stream serializedData)
        {
            using (var reader = new StreamReader(serializedData))
            {
                using (var jsonReader = new Newtonsoft.Json.JsonTextReader(reader))
                {
                    return _Serializer.Deserialize<RpcResponse<T>>(jsonReader);
                }
            }
        }

        /// <summary>
        /// Serializes a <see cref="RpcRequest"/> for transmission to the server.
        /// </summary>
        /// <param name="request">The <see cref="RpcRequest"/> to serialize.</param>
        /// <param name="outputStream">A <see cref="Stream"/> to write the serialized output to.</param>
        /// <remarks>
        /// <para>The <see cref="RpcRequest.Arguments"/> must be one of the following types;
        /// <list type="Bullet">
        /// <item>Dictionary&gt;string, object&lt;</item>
        /// <item>object[]</item>
        /// <item>IEnumerable&lt;KeyValuePair&lt;string, object&gt;&gt;</item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <returns>A <see cref="Stream"/> containing the serialized content.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2202:Do not dispose objects multiple times")]
        public void Serialize(RpcRequest request, Stream outputStream)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (outputStream == null) throw new ArgumentNullException(nameof(outputStream));

            object jsonRpcRequest = RequestToJsonRequest(request);

            using (var nonClosingStream = new NonClosingStreamAdapter(outputStream))
            using (var textWriter = new StreamWriter(nonClosingStream, _TextEncoding))
            using (var writer = new Newtonsoft.Json.JsonTextWriter(textWriter))
            {
                _Serializer.Serialize(writer, jsonRpcRequest);
                writer.Flush();
                textWriter.Flush();
            }
        }

        private static object RequestToJsonRequest(RpcRequest request)
        {
            var argumentsType = request.Arguments?.GetType();

            IEnumerable<KeyValuePair<string, object>> enumerableArgs;
            object jsonRpcRequest;
            if (argumentsType == null || argumentsType == typeof(object[]))
            {
                // Although the Json RPC spec specifically says the arguments parameter
                // may be omitted, some servers do not actually support this. Ensure
                // that even if there are no args we transmit an empty array, to keep
                // compatibility with those servers.
                jsonRpcRequest = new JsonRpcRequest<object[]>()
                {
                    MethodName = request.MethodName,
                    Arguments = (object[])request.Arguments ?? EmptyOrdinalArguments
                };
            }
            else if (argumentsType == typeof(Dictionary<string, object>))
            {
                jsonRpcRequest = new JsonRpcRequest<IEnumerable<KeyValuePair<string, object>>>()
                {
                    MethodName = request.MethodName,
                    Arguments = (IEnumerable<KeyValuePair<string, object>>)request.Arguments
                };
            }
            else if ((enumerableArgs = request.Arguments as IEnumerable<KeyValuePair<string, object>>) != null)
            {
                var argsDict = new Dictionary<string, object>();
                foreach (var kvp in enumerableArgs)
                {
                    argsDict.Add(kvp.Key, kvp.Value);
                }

                jsonRpcRequest = new JsonRpcRequest<IEnumerable<KeyValuePair<string, object>>>()
                {
                    MethodName = request.MethodName,
                    Arguments = argsDict
                };
            }
            else
                throw new ArgumentException(nameof(RpcRequest) + "." + nameof(RpcRequest.Arguments) +
                                            " must be a supported type, usually object[] or Dictionary<string, object>. Check the documentation.");

            return jsonRpcRequest;
        }
    }
}