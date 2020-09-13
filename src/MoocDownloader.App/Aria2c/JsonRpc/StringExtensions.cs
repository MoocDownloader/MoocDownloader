using System;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// Extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        #region ToStream

        /// <summary>
        /// Creates a new (memory) stream containing the UTF-8 bytes from the string specified.
        /// </summary>
        /// <param name="value">The string to use as the content for the stream.</param>
        /// <returns>A <see cref="System.IO.MemoryStream"/> typed as a <see cref="System.IO.Stream"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
        public static System.IO.Stream ToStream(this string value)
        {
            return ToStream(value, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// Creates a new (memory) stream containing the bytes from the string specified, using the provided encoding to convert the string into byte content.
        /// </summary>
        /// <param name="value">The string to use as the content for the stream.</param>
        /// <param name="encoding">The text encoding to use when converting the string into bytes content for the returned stream.</param>
        /// <returns>A <see cref="System.IO.MemoryStream"/> typed as a <see cref="System.IO.Stream"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="value"/> or <paramref name="encoding"/> is null.</exception>
        public static System.IO.Stream ToStream(this string value, System.Text.Encoding encoding)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (encoding == null) throw new ArgumentNullException(nameof(encoding));

            return new System.IO.MemoryStream(encoding.GetBytes(value));
        }

        #endregion
    }
}