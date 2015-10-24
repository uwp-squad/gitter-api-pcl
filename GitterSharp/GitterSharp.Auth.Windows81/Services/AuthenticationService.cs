using GitterSharp.Configuration;
using GitterSharp.Helpers;
using System;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;

namespace GitterSharp.Services
{
    /// <summary>
    /// Service used to finalize the authentication using Web Authentication Broker on Windows 8.1
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields

        private static string _token { get; set; }

        #endregion


        #region Methods

        public async Task<bool?> LoginAsync(string oauthKey, string oauthSecret)
        {
            try
            {
                string startUrl = $"https://gitter.im/login/oauth/authorize?client_id={oauthKey}&response_type=code&redirect_uri={Constants.RedirectUrl}";
                var startUri = new Uri(startUrl);
                var endUri = new Uri(Constants.RedirectUrl);

                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
                _token = await AuthHelper.RetrieveToken(webAuthenticationResult, oauthKey, oauthSecret);

                return !string.IsNullOrWhiteSpace(_token);
            }
            catch
            {
                return null;
            }
        }

        public string RetrieveToken()
        {
            return _token;
        }

        #endregion
    }
}
