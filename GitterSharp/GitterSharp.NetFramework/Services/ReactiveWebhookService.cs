using System;
using GitterSharp.Model;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;
#if __IOS__ || __ANDROID__ || NET45
using System.Net.Http;
using System.Net.Http.Headers;
#endif
#if NETFX_CORE
using Windows.Web.Http;
using Windows.Web.Http.Headers;
#endif

namespace GitterSharp.Services
{
    public class ReactiveWebhookService : IReactiveWebhookService
    {
        #region Fields

        private HttpClient HttpClient
        {
            get
            {
                var httpClient = new HttpClient();

#if __IOS__ || __ANDROID__ || NET45
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
#endif
#if NETFX_CORE
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
#endif

                return httpClient;
            }
        }

        #endregion


        #region Methods

        public IObservable<bool> Post(string url, string message, MessageLevel level = MessageLevel.Info)
        {
            // Create an HttpClient and send content payload
            using (var httpClient = HttpClient)
            {
#if __IOS__ || __ANDROID__ || NET45
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"message", message},
                    {"level", level.ToString().ToLower()}
                });
#endif
#if NETFX_CORE
                var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"message", message},
                    {"level", level.ToString().ToLower()}
                });
#endif

#if __IOS__ || __ANDROID__ || NET45
                return httpClient.PostAsync(new Uri(url), content)
                    .ToObservable()
                    .Select(response => response.IsSuccessStatusCode);
#endif
#if NETFX_CORE
                return httpClient.PostAsync(new Uri(url), content)
                    .AsTask()
                    .ToObservable()
                    .Select(response => response.IsSuccessStatusCode);
#endif
            }
        }

        #endregion
    }
}
