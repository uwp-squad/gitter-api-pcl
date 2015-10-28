using System;
using System.Threading.Tasks;
using GitterSharp.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;

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

                return httpClient;
            }
        }

        #endregion


        #region Methods

        public async Task<bool> Post(string url, string message, MessageLevel level = MessageLevel.Info)
        {
            // Create an HttpClient and send content payload
            using (var httpClient = HttpClient)
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");

                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"message", message},
                    {"level", level.ToString().ToLower()}
                });
                var response = await httpClient.PostAsync(new Uri(url), content);

                var result = JsonConvert.DeserializeObject<WebhookResponse>(await response.Content.ReadAsStringAsync());

                return result.success;
            }
        }

        #endregion
    }
}
