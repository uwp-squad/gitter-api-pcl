using System;
using Windows.Web.Http;

namespace GitterSharp.Exceptions
{
    /// <summary>
    /// Api Exception designed using octokit.net example
    /// https://github.com/octokit/octokit.net/blob/1266ac0f3a366f033061d0c1cc0785bc3c9f5bd3/Octokit/Exceptions/ApiException.cs
    /// </summary>
    public class ApiException : Exception
    {
        #region Properties

        private readonly string _message;
        /// <summary>
        /// The content message of the error
        /// </summary>
        public override string Message { get { return _message; } }

        /// <summary>
        /// The HTTP status code associated with the repsonse
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        #endregion


        #region Constructors

        public ApiException() { }

        public ApiException(string message)
        {
            _message = message;
        }

        public ApiException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, HttpStatusCode statusCode)
        {
            _message = message;
            StatusCode = statusCode;
        }

        #endregion
    }
}
