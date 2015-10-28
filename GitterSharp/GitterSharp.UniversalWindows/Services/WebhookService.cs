using System.Threading.Tasks;
using GitterSharp.Model;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using System;
using System.Collections.Generic;

namespace GitterSharp.Services
{
    public class WebhookService : IWebhookService
    {
        #region Fields

        private HttpClient HttpClient
        {
            get
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));

                return httpClient;
            }
        }

        #endregion


        #region Methods

        public async Task<bool> PostAsync(string url, string message, MessageLevel level = MessageLevel.Info)
        {
            // Create an HttpClient and send content payload
            using (var httpClient = HttpClient)
            {
                var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"message", message},
                    {"level", level.ToString().ToLower()}
                });
                var response = await httpClient.PostAsync(new Uri(url), content);

                return response.IsSuccessStatusCode;
            }
        }

        #endregion
    }
}
