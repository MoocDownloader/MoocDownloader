namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Used by <see cref="RpcResponse{T}"/> to generate unique id's.
    /// </summary>
    public static class JsonRpcRequestIdGenerator
    {
        #region Static Unique ID Generation Stuff

        private static volatile int _RequestId;

        /// <summary>
        /// Returns a new unique identifier, in a thread-safe manner.
        /// </summary>
        /// <returns>Returns a <see cref="System.Int32"/>.</returns>
        public static int GenerateUniqueId()
        {
            return System.Threading.Interlocked.Increment(ref _RequestId);
        }

        /// <summary>
        /// Resets the automatically generated unique id counter for requests. Normally only useful for testing, or in systems where an extremely large number of requests have happened since last restart.
        /// </summary>
        public static void ResetId()
        {
            System.Threading.Interlocked.Exchange(ref _RequestId, 0);
        }

        #endregion
    }
}