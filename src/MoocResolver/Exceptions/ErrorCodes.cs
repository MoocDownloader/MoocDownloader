namespace MoocResolver.Exceptions;

public static class ErrorCodes
{
    public static class Browser
    {
        /// <summary>
        /// Browser initialization failed.
        /// </summary>
        public const string InitializationFailed = "Browser:00001";
    }

    public static class Resolver
    {
        /// <summary>
        /// Page load failed.
        /// </summary>
        public const string CannotAccessPage = "Resolver:00001";

        /// <summary>
        /// Credentials have wrong.
        /// </summary>
        public const string WrongCredentials = "Resolver:00002";
    }

    public static class ICOURSE163
    {
        /// <summary>
        /// Cannot parse the course id from the page.
        /// </summary>
        public const string CannotParseCourseId = "ICOURSE163:00001";
    }
}