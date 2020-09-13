using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// An generalized implementation of the <see cref="IRpcClient"/> interface.
    /// </summary>
    /// <remarks>
    /// <para>While clients could depend on this generalized representation of an RPC client, we recommend using <see cref="IRpcClient"/> for references, to ensure maximum flexibility when changing implementations in the future.</para>
    /// </remarks>
    public class RpcClient : IRpcClient
    {
        private RpcClientOptions _Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcClient"/> class.
        /// </summary>
        /// <remarks>
        /// <para>Note, the <paramref name="options"/> reference passed to the constructor should NOT be modified after this instance is constructed. It 
        /// will be accessed by this instance in an unsynchronized manner, and modifying it after client construction may result in race conditions causing errors.
        /// </para>
        /// </remarks>
        /// <param name="options">The <see cref="RpcClientOptions"/> instance used to configure this client..</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="options" /> is null, or any required property of <paramref name="options"/> is null.</exception>
        public RpcClient(RpcClientOptions options)
        {
            options.GuardNull(nameof(options));
            options.Transport.GuardNull(nameof(options) + "." + nameof(options.Transport));
            options.Serializer.GuardNull(nameof(options) + "." + nameof(options.Serializer));

            _Options = options;
        }

        /// <summary>
        /// Invokes the specified remote method with no arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        public async Task<T> Invoke<T>(string methodName)
        {
            methodName.GuardNullOrEmpty(nameof(methodName));

            //Create a request
            var request = new RpcRequest()
            {
                MethodName = methodName,
            };

            return await SendRequest<T>(methodName, request).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes the specified remote method with named arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <param name="arguments">A dictionary of arguments, where the key is the argument name.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        public async Task<T> Invoke<T>(string methodName, params object[] arguments)
        {
            methodName.GuardNullOrEmpty(nameof(methodName));

            //Create a request
            var request = new RpcRequest()
            {
                MethodName = methodName,
                Arguments = arguments
            };

            return await SendRequest<T>(methodName, request).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes the specified remote method with named arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <param name="arguments">A dictionary of arguments, where the key is the argument name.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        public async Task<T> Invoke<T>(string methodName, IDictionary<string, object> arguments)
        {
            methodName.GuardNullOrEmpty(nameof(methodName));

            //Create a request
            var request = new RpcRequest()
            {
                MethodName = methodName,
                Arguments = arguments
            };

            return await SendRequest<T>(methodName, request).ConfigureAwait(false);
        }

        /// <summary>
        /// Invokes the specified remote method with named arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <param name="arguments">An IEnumerable&lt;KeyValuePair&lt;string, object&gt;&gt; of arguments, where the key is the argument name.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        public async Task<T> Invoke<T>(string methodName, IEnumerable<KeyValuePair<string, object>> arguments)
        {
            methodName.GuardNullOrEmpty(nameof(methodName));

            //Create a request
            var request = new RpcRequest()
            {
                MethodName = methodName,
                Arguments = arguments
            };

            return await SendRequest<T>(methodName, request).ConfigureAwait(false);
        }

        private async Task<T> SendRequest<T>(string methodName, RpcRequest request)
        {
            //Serialize request
            using (var stream = new System.IO.MemoryStream())
            {
                _Options.Serializer.Serialize(request, stream);
                stream.Seek(0, System.IO.SeekOrigin.Begin);

                //SendRequest
                using (var responseStream = await _Options.Transport.SendRequest(stream).ConfigureAwait(false))
                {
                    // Deserialize the response
                    var rpcResult = _Options.Serializer.Deserialize<T>(responseStream);

                    // Report error if one occurred
                    if (rpcResult.Error != null)
                        throw new RpcException(methodName, rpcResult.Error);

                    //Return the result
                    return rpcResult.Result;
                }
            }
        }
    }
}