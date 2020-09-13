using System;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Represents an exception thrown if the RPC server returns an error response.
    /// </summary>
#if SUPPORTS_SERIALIZATION
	[System.Serializable]
#endif
    public sealed class RpcException : Exception
    {
        private RpcError _RpcError;

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class.
        /// </summary>
        public RpcException() : base("Unknown RPC error")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class with a custom error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public RpcException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class with a custom error message and inner exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public RpcException(string message, Exception inner) : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpcException"/> class with the name of the method that failed and the error response from the RPC server.
        /// </summary>
        /// <param name="methodName">The name of the remote method that failed.</param>
        /// <param name="error">A <see cref="RpcError"/> instance containing details of the error.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RpcException(string methodName, RpcError error) : base(error?.Message ?? "Unknown RPC error")
        {
            Data["MethodName"] = methodName;
            Data["ErrorCode"] = error?.Code;
            _RpcError = error;
        }

#if SUPPORTS_SERIALIZATION
		protected RpcException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

#endif

        /// <summary>
        /// Gets the name of the remote method that returned an error.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName => Data.Contains("MethodName") ? (string)Data["MethodName"] : String.Empty;

        /// <summary>
        /// Gets the <see cref="RpcError"/> describing detailed information about the error that occurred.
        /// </summary>
        /// <remarks>
        /// <para>This property is not serializable on all platforms, and may not pass across app domain/remoting boundaries etc.</para>
        /// </remarks>
        /// <value>The RPC error.</value>
        public RpcError RpcError => _RpcError;

        /// <summary>
        /// Gets the RPC error code returned from the server, if any.
        /// </summary>
        /// <value>The RPC error as a nullable integer. Null if no error code was returned.</value>
        public int? ErrorCode => Data.Contains("ErrorCode") ? (int?)Data["ErrorCode"] : null;
    }
}