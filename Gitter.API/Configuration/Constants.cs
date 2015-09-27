namespace Gitter.API.Configuration
{
    public static class Constants
    {
        /// <summary>
        /// Base URL of the Gitter API
        /// </summary>
        public static string ApiBaseUrl = "https://api.gitter.im/";

        /// <summary>
        /// Base URL of the streaming Gitter API
        /// </summary>
        public static string StreamApiBaseUrl = "https://stream.gitter.im/";

        /// <summary>
        /// Version of the current Gitter API
        /// </summary>
        public static string ApiVersion = "v1/";

        /// <summary>
        /// Redirect URL when authenticate
        /// </summary>
        public static string RedirectUrl = "http://localhost";
    }
}
