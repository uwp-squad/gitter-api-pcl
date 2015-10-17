using System;
using System.Threading.Tasks;
using GitterSharp.Configuration;
using Windows.Security.Authentication.Web;

namespace GitterSharp.Services
{
    public partial class GitterApiService : BaseGitterApiService
    {
        #region Authentication

        public override async Task<bool?> LoginAsync(string oauthKey, string oauthSecret)
        {
            try
            {
                AuthenticationService.OauthKey = oauthKey;
                AuthenticationService.OauthSecret = oauthSecret;

                string startUrl = $"https://gitter.im/login/oauth/authorize?client_id={oauthKey}&response_type=code&redirect_uri={Constants.RedirectUrl}";
                var startUri = new Uri(startUrl);
                var endUri = new Uri(Constants.RedirectUrl);

                WebAuthenticationBroker.AuthenticateAndContinue(startUri, endUri, null, WebAuthenticationOptions.None);

                return await Task.FromResult<bool?>(null);                
            }
            catch
            {
                return null;
            }
        }
        
        #endregion
    }
}
