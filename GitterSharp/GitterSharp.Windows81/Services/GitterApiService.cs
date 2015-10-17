using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GitterSharp.Configuration;
using GitterSharp.Helpers;
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
                string startUrl = $"https://gitter.im/login/oauth/authorize?client_id={oauthKey}&response_type=code&redirect_uri={Constants.RedirectUrl}";
                var startUri = new Uri(startUrl);
                var endUri = new Uri(Constants.RedirectUrl);

                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri, endUri);
                string token = await AuthHelper.RetrieveToken(webAuthenticationResult, oauthKey, oauthSecret);

                SetToken(token);

                return !string.IsNullOrWhiteSpace(token);
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
}
