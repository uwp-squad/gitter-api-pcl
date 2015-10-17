using GitterSharp.Helpers;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace GitterSharp.Services
{
    /// <summary>
    /// Service used to finalize the authentication using Web Authentication Broker on Windows Phone 8.1
    /// </summary>
    public static class AuthenticationService
    {
        #region Fields

        public static string OauthKey { get; set; }
        public static string OauthSecret { get; set; }

        #endregion


        #region Methods

        public static async Task<string> RetrieveTokenAsync(WebAuthenticationBrokerContinuationEventArgs args)
        {
            try
            {
                return await AuthHelper.RetrieveToken(args.WebAuthenticationResult, OauthKey, OauthSecret);
            }
            catch
            {
            }
            finally
            {
                OauthKey = string.Empty;
                OauthSecret = string.Empty;
            }

            return null;
        }

        #endregion
    }
}
