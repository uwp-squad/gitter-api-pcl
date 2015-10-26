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

                return httpClient;
            }
        }

        #endregion


        #region Methods

        public async Task<bool> Post(string url, string message, MessageLevel level = MessageLevel.Info)
        {
            string result;

            // Create an HttpClient and send content payload
            using (var httpClient = HttpClient)
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"message", message}
                });
                var response = await httpClient.PostAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                result = await response.Content.ReadAsStringAsync();
            }

            return (result == "ok");
        }

        #endregion
    }
}
