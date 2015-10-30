using GitterSharp.Exceptions;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
#if __IOS__ || __ANDROID__ || NET45
using System.Net.Http;
using HttpContent = System.Net.Http.HttpContent;
#endif
#if NETFX_CORE
using Windows.Web.Http;
using HttpContent = Windows.Web.Http.IHttpContent;
#endif

namespace GitterSharp.Helpers
{
    internal static class HttpHelper
    {
        public static async Task<T> GetAsync<T>(this HttpClient httpClient, string url)
        {
            using (httpClient)
            {
                var response = await httpClient.GetAsync(new Uri(url));

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static async Task PostAsync(this HttpClient httpClient, string url, HttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PostAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);
            }
        }
        public static async Task<T> PostAsync<T>(this HttpClient httpClient, string url, HttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PostAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static async Task PutAsync(this HttpClient httpClient, string url, HttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PutAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);
            }
        }
        public static async Task<T> PutAsync<T>(this HttpClient httpClient, string url, HttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PutAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);

                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(result);
            }
        }
    }
}