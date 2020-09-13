using System.Threading.Tasks;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// An interface for components that perform actual network transmission of the request data.
    /// </summary>
    /// <remarks>
    /// <para>IRpcTransport components are responsible for actually sending and receiving data to/from the server, i.e using HTTP or sockets etc. They 
    /// know nothing about the format of the data sent, just how to transmit it.
    /// </para>
    /// </remarks>
    public interface IRpcTransport
    {
        /// <summary>
        /// Transmits a serialized request to the remote server and returns the response as a stream.
        /// </summary>
        /// <param name="requestContent">The content of the request.</param>
        /// <returns>An awaitable Task&lt;System.IO.Stream&gt; containing the response stream.</returns>
        Task<System.IO.Stream> SendRequest(System.IO.Stream requestContent);
    }
}