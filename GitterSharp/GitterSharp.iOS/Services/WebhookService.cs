using System;
using System.Threading.Tasks;
using GitterSharp.Model;
using System.Net.Http;
using System.Net.Http.Headers;
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
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

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
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
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
