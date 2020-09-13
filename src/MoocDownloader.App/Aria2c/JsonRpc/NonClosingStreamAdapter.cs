using System;
using System.IO;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// NonClosingStreamAdapter wraps an existing stream instance and passes through all calls except for <see cref="Dispose(bool)"/>, which is ignored. This
    /// is useful when you must pass a stream to another component that insists on closing the stream under various conditions, but you want to retain access 
    /// to the stream after you're done with that component.
    /// </summary>
    /// <seealso cref="System.IO.Stream" />
    public class NonClosingStreamAdapter : Stream
    {
        #region Fields

        private readonly Stream _Stream;

        #endregion

        #region Constuctors

        /// <summary>
        /// Initializes a new instance of the <see cref="NonClosingStreamAdapter"/> class wrapping the provided stream instance.
        /// </summary>
        /// <param name="stream">The stream to wrap with this adapter instance.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        public NonClosingStreamAdapter(Stream stream) =>
            _Stream = stream ?? throw new ArgumentNullException(nameof(stream));

        #endregion

        #region Overrides

        /// <summary>
        /// Calls <see cref="System.Object.ToString()"/> on the wrapped stream.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents the wrapped stream.</returns>
        public override string ToString()
        {
            return _Stream.ToString();
        }

        /// <summary>
        /// Sets or returns the <see cref="System.IO.Stream.WriteTimeout"/> value on the wrapped stream.
        /// </summary>
        /// <value>The write timeout.</value>
        public override int WriteTimeout
        {
            get => _Stream.WriteTimeout;

            set => _Stream.WriteTimeout = value;
        }

        /// <summary>
        /// Calls <see cref="WriteByte"/> on the wrapped stream.
        /// </summary>
        /// <param name="value">The byte to write to the stream.</param>
        public override void WriteByte(byte value)
        {
            _Stream.WriteByte(value);
        }

        /// <summary>
        /// Calls <see cref="ReadByte"/> on the wrapped stream.
        /// </summary>
        /// <returns>The unsigned byte cast to an Int32, or -1 if at the end of the stream.</returns>
        public override int ReadByte()
        {
            return _Stream.ReadByte();
        }

        /// <summary>
        /// Sets or returns the <see cref="System.IO.Stream.ReadTimeout"/> value on the wrapped stream.
        /// </summary>
        /// <value>The read timeout.</value>
        public override int ReadTimeout
        {
            get => _Stream.ReadTimeout;

            set => _Stream.ReadTimeout = value;
        }

        /// <summary>
        /// Returns the <see cref="System.IO.Stream.CanTimeout"/> value from the wrapped stream.
        /// </summary>
        /// <value><c>true</c> if this instance can timeout; otherwise, <c>false</c>.</value>
        public override bool CanTimeout => _Stream.CanTimeout;

        /// <summary>
        /// Returns the <see cref="System.IO.Stream.CanRead"/> value from the wrapped stream.
        /// </summary>
        /// <value><c>true</c> if this instance can read; otherwise, <c>false</c>.</value>
        public override bool CanRead => _Stream.CanRead;

        /// <summary>
        /// Returns the <see cref="System.IO.Stream.CanSeek"/> value from the wrapped stream.
        /// </summary>
        /// <value><c>true</c> if this instance can seek; otherwise, <c>false</c>.</value>
        public override bool CanSeek => _Stream.CanSeek;

        /// <summary>
        /// Returns the <see cref="System.IO.Stream.CanWrite"/> value from the wrapped stream.
        /// </summary>
        /// <value><c>true</c> if this instance can write; otherwise, <c>false</c>.</value>
        public override bool CanWrite => _Stream.CanWrite;

        /// <summary>
        /// Returns the <see cref="System.IO.Stream.Length"/> value from the wrapped stream.
        /// </summary>
        /// <value>The total length of the stream, if known.</value>
        public override long Length => _Stream.Length;

        /// <summary>
        /// Sets or returns the <see cref="System.IO.Stream.Position"/> value on the wrapped stream, representing the current position within the stream.
        /// </summary>
        /// <value>The position within the stream.</value>
        public override long Position
        {
            get => _Stream.Position;

            set => _Stream.Position = value;
        }

        /// <summary>
        /// Calls <see cref="System.IO.Stream.Flush"/> on the wrapped stream.
        /// </summary>
        public override void Flush()
        {
            _Stream.Flush();
        }

        /// <summary>
        /// Calls the <see cref="System.IO.Stream.Read"/> method on the wrapped stream.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset" /> and (<paramref name="offset" /> + <paramref name="count" /> - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _Stream.Read(buffer, offset, count);
        }

        /// <summary>
        /// Calls the <see cref="System.IO.Stream.Seek"/> method on the wrapped stream.
        /// </summary>
        /// <param name="offset">A byte offset relative to the <paramref name="origin" /> parameter.</param>
        /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin" /> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current stream.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _Stream.Seek(offset, origin);
        }

        /// <summary>
        /// Calls the <see cref="System.IO.Stream.SetLength(long)"/> method on the wrapped stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
            _Stream.SetLength(value);
        }

        /// <summary>
        /// Calls the <see cref="System.IO.Stream.Write"/> method on the wrapped stream.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies <paramref name="count" /> bytes from <paramref name="buffer" /> to the current stream.</param>
        /// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> at which to begin copying bytes to the current stream.</param>
        /// <param name="count">The number of bytes to be written to the current stream.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            _Stream.Write(buffer, offset, count);
        }

        /// <summary>
        /// Does nothing, ensuring the wrapped stream is not closed/disposed.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage",
            "CA2215:Dispose methods should call base class dispose",
            Justification = "This method deliberately does nothing.")]
        protected override void Dispose(bool disposing)
        {
        }

#if NETSTANDARD
		/// <summary>
		/// Calls <see cref="System.IO.Stream.WriteAsync(byte[], int, int, CancellationToken)"/> on the wrapped stream.
		/// </summary>
		/// <param name="buffer">The buffer to write data from.</param>
		/// <param name="offset">The zero-based byte offset in <paramref name="buffer" /> from which to begin copying bytes to the stream.</param>
		/// <param name="count">The maximum number of bytes to write.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous write operation.</returns>
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return base.WriteAsync(buffer, offset, count, cancellationToken);
		}
		/// <summary>
		/// Calls <see cref="System.IO.Stream.ReadAsync(byte[], int, int, CancellationToken)"/> on the wrapped stream.
		/// </summary>
		/// <param name="buffer">The buffer to write the data into.</param>
		/// <param name="offset">The byte offset in <paramref name="buffer" /> at which to begin writing data from the stream.</param>
		/// <param name="count">The maximum number of bytes to read.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous read operation. The value of the result contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the stream has been reached.</returns>
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return _Stream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		/// <summary>
		/// Calls <see cref="FlushAsync(CancellationToken)"/> on the wrapped stream.
		/// </summary>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous flush operation.</returns>
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return _Stream.FlushAsync();
		}

		/// <summary>
		/// Calls <see cref="System.IO.Stream.CopyToAsync(Stream, int, CancellationToken)"/> on the wrapped stream.
		/// </summary>
		/// <param name="destination">The stream to which the contents of the current stream will be copied.</param>
		/// <param name="bufferSize">The size, in bytes, of the buffer. This value must be greater than zero. The default size is 81920.</param>
		/// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="P:System.Threading.CancellationToken.None" />.</param>
		/// <returns>A task that represents the asynchronous copy operation.</returns>
		public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken)
		{
			return _Stream.CopyToAsync(destination, bufferSize, cancellationToken);
		}

#endif

        #endregion
    }
}