namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Extension methods for byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Creates a new <see cref="System.IO.MemoryStream"/> using the byte array as the stream contents.
        /// </summary>
        /// <param name="buffer">The byte array to wrap in a stream.</param>
        /// <returns>A <see cref="System.IO.MemoryStream"/>.</returns>
        public static System.IO.MemoryStream ToStream(this byte[] buffer)
        {
            return new System.IO.MemoryStream(buffer);
        }
    }
}