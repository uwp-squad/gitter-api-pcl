using System.Threading.Tasks;

namespace GitterSharp.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Execute login process through OAuth2 authentication mechanism
        /// (https://developer.gitter.im/docs/authentication)
        /// </summary>
        /// <returns>true: login success / false: login failed / null: exception occured</returns>
        Task<bool?> LoginAsync(string oauthKey, string oauthSecret);
    }
}
