using System.Threading.Tasks;
using GitterSharp.Model;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using System;
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
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

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
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));

                var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
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
