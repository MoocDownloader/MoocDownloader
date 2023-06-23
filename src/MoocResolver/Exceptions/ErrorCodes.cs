namespace MoocResolver.Exceptions;

public static class ErrorCodes
{
    public static class Browser
    {
        /// <summary>
        /// Browser initialization failed.
        /// </summary>
        public const string InitializationFailed = "Browser:01";
    }

    public static class Resolver
    {
        /// <summary>
        /// Do not support to resolve the given URL.
        /// </summary>
        public const string NotSupport = "Resolver:01";

        /// <summary>
        /// Unable to access the page that the URL entered by user.
        /// </summary>
        public const string UnableToAccessPage = "Resolver:02";

        /// <summary>
        /// Login failed.
        /// </summary>
        public const string LoginFailed = "Resolver:03";

        /// <summary>
        /// Credentials have wrong.
        /// </summary>
        public const string WrongCredentials = "Resolver:04";

        /// <summary>
        /// Can not resolve the given URL.
        /// </summary>
        public const string CannotResolve = "Resolver:05";
    }

    public static class ICOURSE163
    {
        /// <summary>
        /// Cannot parse the course id from the page.
        /// </summary>
        public const string CannotParseCourseId = "ICOURSE163:01";
    }
}