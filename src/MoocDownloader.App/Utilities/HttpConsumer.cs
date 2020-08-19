namespace MoocDownloader.App.Utilities
{
    /// <summary>
    /// The return type.
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// Represents that only strings are returned.
        /// </summary>
        String,

        /// <summary>
        /// Represents that both of string and byte are returned.
        /// </summary>
        Byte
    }

    /// <summary>
    /// The post request body data type. The default is string type.
    /// </summary>
    public enum PostBodyType
    {
        /// <summary>
        /// The string type. The encoding need not be set when the type is a string type.
        /// </summary>
        String,

        /// <summary>
        /// The byte array type. The encoding parameter can be null.
        /// </summary>
        Bytes,

        /// <summary>
        /// Post a file. The PostBody property should be set to the absolute path of the file.
        /// </summary>
        FilePath
    }

    /// <summary>
    /// Cookie return type.
    /// </summary>
    public enum ResultCookieType
    {
        /// <summary>
        /// The string type.
        /// </summary>
        String,

        /// <summary>
        /// The collection of cookie.
        /// </summary>
        CookieCollection
    }

    /// <summary>
    /// HTTP request method.
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// CONNECT.
        /// </summary>
        CONNECT,

        /// <summary>
        /// DELETE.
        /// </summary>
        DELETE,

        /// <summary>
        /// GET.
        /// </summary>
        GET,

        /// <summary>
        /// HEAD.
        /// </summary>
        HEAD,

        /// <summary>
        /// OPTIONS.
        /// </summary>
        OPTIONS,

        /// <summary>
        /// PATCH.
        /// </summary>
        PATCH,

        /// <summary>
        /// POST.
        /// </summary>
        POST,

        /// <summary>
        /// PUT.
        /// </summary>
        PUT,

        /// <summary>
        /// TRACE.
        /// </summary>
        TRACE,
    }
}