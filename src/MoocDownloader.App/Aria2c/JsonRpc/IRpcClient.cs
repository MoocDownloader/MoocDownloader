using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoocDownloader.App.Aria2c.JsonRpc
{
    /// <summary>
    /// A generalized interface for RPC clients. Allows making remote procedure calls in a protocol &amp; format agnostic fashion.
    /// </summary>
    /// <remarks>
    /// <para>Generally client code should refer to RpcClient implementations using a reference of this type. This protects client code against changes to 
    /// the specific format or transport mechansim used, making it easier to change implementations in the future.</para>
    /// </remarks>
    public interface IRpcClient
    {
        /// <summary>
        /// Invokes the specified remote method with no arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        Task<T> Invoke<T>(string methodName);

        /// <summary>
        /// Invokes the specified remote method with named arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <param name="arguments">A dictionary of arguments, where the key is the argument name.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        Task<T> Invoke<T>(string methodName, IDictionary<string, object> arguments);

        /// <summary>
        /// Invokes the specified remote method with named arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <param name="arguments">An IEnumerable&lt;KeyValuePair&lt;string, object&gt;&gt; of arguments, where the key is the argument name.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<T> Invoke<T>(string methodName, IEnumerable<KeyValuePair<string, object>> arguments);

        /// <summary>
        /// Invokes the specified remote method with positional arguments.
        /// </summary>
        /// <typeparam name="T">The type of response expected.</typeparam>
        /// <param name="methodName">The name of the remote method to call.</param>
        /// <param name="arguments">An object array whose values correspond to the remote methods arguments by ordinal position.</param>
        /// <returns>An awaitable Task&lt;T&gt; that will contain the result.</returns>		
        Task<T> Invoke<T>(string methodName, params object[] arguments);
    }
}