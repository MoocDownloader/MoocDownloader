using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Extension methods for streams.
    /// </summary>
    public static class StreamExtensions
    {
        #region WriteAllBytes

        /// <summary>
        /// Writes all bytes in the specified byte array to the stream.
        /// </summary>
        /// <param name="data">The bytes to be written.</param>
        /// <param name="stream">The stream.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="data"/> or <paramref name="stream"/> is null.</exception>
        public static void WriteAllBytes(this Stream stream, byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            if (data.Length == 0) return;

            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Writes all bytes in the specified byte array to the stream.
        /// </summary>
        /// <param name="data">The bytes to be written.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>The <see cref="System.Threading.Tasks.Task" /> instance that can be awaited.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="data"/> or <paramref name="stream"/> is null.</exception>
        public static async Task WriteAllBytesAsync(this Stream stream, byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            if (data.Length == 0) return;

            await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
        }

        #endregion

        #region ReadAllBytes

        /// <summary>
        /// Performs a 'naive' read of the stream from it's current position to it's end and returns a byte array with the contents. 
        /// </summary>
        /// <remarks>
        /// <para>This method should only be used on streams known to be small, as it reads all data in one go into a byte array in memory. Performing this on streams with large content, or where the stream length is unknown will cause problems.</para>
        /// </remarks>
        /// <param name="stream">The stream.</param>
        /// <returns>A byte array containing the stream contents.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var remainingBytes = stream.Length - stream.Position;
            if (remainingBytes == 0) return new byte[] { };

            var retVal = new byte[remainingBytes];
            stream.Read(retVal, 0, retVal.Length);
            return retVal;
        }

        /// <summary>
        /// Performs a 'naive' read of the stream from it's current position to it's end and returns a byte array with the contents. 
        /// </summary>
        /// <remarks>
        /// <para>This method should only be used on streams known to be small, as it reads all data in one go into a byte array in memory. Performing this on streams with large content, or where the stream length is unknown will cause problems.</para>
        /// </remarks>
        /// <param name="stream">The stream.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> whose result is a byte array containing the stream contents.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        public static async Task<byte[]> ReadAllBytesAsync(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            var remainingBytes = stream.Length - stream.Position;
            if (remainingBytes == 0) return new byte[] { };

            var retVal = new byte[remainingBytes];
            await stream.ReadAsync(retVal, 0, retVal.Length).ConfigureAwait(false);
            return retVal;
        }

        #endregion

        #region ReadAsString

        /// <summary>
        /// Reads the contents of stream from it's current point to the end and returns the result as a string.
        /// </summary>
        /// <remarks>
        /// <para>This overload uses the <see cref="System.Text.UTF8Encoding"/> to convert the stream contents into a string.</para>
        /// </remarks>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A <see cref="System.String"/> representation of the stream contents.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the stream length is greater than <see cref="int.MaxValue"/>.</exception>
        public static string ReadAsString(this Stream stream)
        {
            return ReadAsString(stream, Encoding.UTF8);
        }

        /// <summary>
        /// Reads the contents of stream from it's current point to the end and returns the result as a string.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="encoding">The text encoding to use when decoding the stream into a string.</param>
        /// <returns>A <see cref="string"/> representation of the stream contents.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="stream"/> or <paramref name="encoding"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if the stream length is greater than <see cref="int.MaxValue"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2202:Do not dispose objects multiple times")]
        public static string ReadAsString(Stream stream, Encoding encoding)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));

            if (stream.Length == 0) return String.Empty;

            var stringSize = stream.Length - stream.Position;
            if (stringSize <= 0) return String.Empty;

            if (stringSize > Int32.MaxValue)
                throw new InvalidOperationException("Stream too large to convert to string.");

            using (var wrapper = new NonClosingStreamAdapter(stream))
            {
                using (var reader = new StreamReader(wrapper, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Reads the contents of stream from it's current point to the end and returns the result as a string.
        /// </summary>
        /// <remarks>
        /// <para>This overload uses the <see cref="System.Text.UTF8Encoding"/> to convert the stream contents into a string.</para>
        /// </remarks>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> whose result is the string representation of the stream contents.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the stream length is greater than <see cref="int.MaxValue"/>.</exception>
        public static Task<string> ReadAsStringAsync(this Stream stream)
        {
            return ReadAsStringAsync(stream, Encoding.UTF8);
        }

        /// <summary>
        /// Reads the contents of stream from it's current point to the end and returns the result as a string.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <param name="encoding">The text encoding to use when decoding the stream into a string.</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> whose result is the string representation of the stream contents.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="stream"/> or <paramref name="encoding"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the stream length is greater than <see cref="int.MaxValue"/>.</exception>
        public static async Task<string> ReadAsStringAsync(Stream stream, Encoding encoding)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));

            if (stream.Length == 0) return String.Empty;

            var stringSize = stream.Length - stream.Position;
            if (stringSize <= 0) return String.Empty;

            if (stringSize > Int32.MaxValue)
                throw new InvalidOperationException("Stream too large to convert to string.");

            using (var wrapper = new NonClosingStreamAdapter(stream))
            {
                using (var reader = new StreamReader(wrapper, encoding))
                {
                    return await reader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
        }

        #endregion
    }
}