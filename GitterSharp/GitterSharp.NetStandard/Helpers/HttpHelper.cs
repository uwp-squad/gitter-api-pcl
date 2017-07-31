using GitterSharp.Exceptions;
using Newtonsoft.Json;
using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using System.Net.Http;
using HttpContent = System.Net.Http.HttpContent;

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

        public static async Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string url, HttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PostAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);

                return response;
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

        public static async Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string url, HttpContent content)
        {
            using (httpClient)
            {
                var response = await httpClient.PutAsync(new Uri(url), content);

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);

                return response;
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

        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string url)
        {
            using (httpClient)
            {
                var response = await httpClient.GetAsync(new Uri(url));

                if (!response.IsSuccessStatusCode)
                    throw new ApiException(response.ReasonPhrase, response.StatusCode);

                return response;
            }
        }
        public static async Task<T> DeleteAsync<T>(this HttpClient httpClient, string url)
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

        public static IObservable<T> CreateObservableHttpStream<T>(this HttpClient httpClient, string url)
        {
            return Observable.Using(() => httpClient,
                client => client.GetStreamAsync(new Uri(url))
                    .ToObservable()
                    .Select(x => Observable.FromAsync(() => StreamHelper.ReadStreamAsync(x)).Repeat())
                    .Concat()
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(JsonConvert.DeserializeObject<T>));
        }
    }
}