using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace GitterSharp.Helpers
{
    internal static class HttpHelper
    {
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url)
        {
            using (httpClient)
            {
                var response = await httpClient.GetAsync(new Uri(url));

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(result);
                }

                throw new Exception();
            }
        }

        public static async Task PostAsync(this HttpClient httpClient, string url, IHttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PostAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }
        }
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string url, IHttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PostAsync(new Uri(url), content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(result);
                }

                throw new Exception();
            }
        }

        public static async Task PutAsync(this HttpClient httpClient, string url, IHttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PutAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();
            }
        }
        public static async Task<T> PutAsync<T>(this HttpClient httpClient, string url, IHttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PutAsync(new Uri(url), content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(result);
                }

                throw new Exception();
            }
        }
    }
}
