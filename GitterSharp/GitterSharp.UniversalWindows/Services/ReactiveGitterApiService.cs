using GitterSharp.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using GitterSharp.Model;
using System.Reactive;
using Windows.Web.Http;
using GitterSharp.Configuration;
using Windows.Web.Http.Headers;
using System.Reactive.Linq;
using Newtonsoft.Json;
using System.Reactive.Threading.Tasks;
using GitterSharp.Helpers;
using System.IO;
using Windows.Storage.Streams;

namespace GitterSharp.UniversalWindows.Services
{
    public class ReactiveGitterApiService : IReactiveGitterApiService
    {
        #region Fields

        private readonly string _baseApiAddress = $"{Constants.ApiBaseUrl}{Constants.ApiVersion}";
        private readonly string _baseStreamingApiAddress = $"{Constants.StreamApiBaseUrl}{Constants.ApiVersion}";

        private HttpClient HttpClient
        {
            get
            {
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(Token))
                    httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Token);

                return httpClient;
            }
        }

        #endregion

        #region Properties

        public string Token { get; set; }

        #endregion

        #region User

        public IObservable<User> GetCurrentUserAsync()
        {
            string url = _baseApiAddress + "user";
            return HttpClient.GetAsync<IEnumerable<User>>(url)
                .AsAsyncOperation()
                .GetResults()
                .ToObservable();
        }

        public IObservable<IEnumerable<Organization>> GetOrganizationsAsync(string userId)
        {
            string url = _baseApiAddress + $"user/{userId}/orgs";
            return HttpClient.GetAsync<IEnumerable<Organization>>(url)
                .ToObservable();
        }

        public IObservable<IEnumerable<Repository>> GetRepositoriesAsync(string userId)
        {
            string url = _baseApiAddress + $"user/{userId}/repos";
            return HttpClient.GetAsync<IEnumerable<Repository>>(url)
                .ToObservable();
        }

        #endregion

        #region Unread Items

        public IObservable<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/unreadItems";
            return HttpClient.GetAsync<UnreadItems>(url)
                .ToObservable();
        }

        public IObservable<Unit> MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/unreadItems";
            var content = new HttpStringContent("{\"chat\": " + JsonConvert.SerializeObject(messageIds) + "}",
                UnicodeEncoding.Utf8,
                "application/json");

            return HttpClient.PostAsync(url, content)
                .ToObservable();
        }

        #endregion

        #region Rooms

        public IObservable<IEnumerable<Room>> GetRoomsAsync()
        {
            string url = _baseApiAddress + "rooms";
            return HttpClient.GetAsync<IEnumerable<Room>>(url)
                .ToObservable();
        }

        public IObservable<Room> JoinRoomAsync(string roomName)
        {
            string url = _baseApiAddress + "rooms";
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                {"uri", roomName}
            });

            return HttpClient.PostAsync<Room>(url, content)
                .ToObservable();
        }

        #endregion

        #region Messages

        public IObservable<Message> GetSingleRoomMessageAsync(string roomId, string messageId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}";
            return HttpClient.GetAsync<Message>(url)
                .ToObservable();
        }

        public IObservable<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages?limit={limit}";

            if (!string.IsNullOrWhiteSpace(beforeId))
                url += $"&beforeId={beforeId}";

            if (!string.IsNullOrWhiteSpace(afterId))
                url += $"&afterId={afterId}";

            if (skip > 0)
                url += $"&skip={skip}";

            return HttpClient.GetAsync<IEnumerable<Message>>(url)
                .ToObservable();
        }

        public IObservable<Message> SendMessageAsync(string roomId, string message)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages";
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });

            return HttpClient.PostAsync<Message>(url, content)
                .ToObservable();
        }

        public IObservable<Message> UpdateMessageAsync(string roomId, string messageId, string message)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}";
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });

            return HttpClient.PutAsync<Message>(url, content)
                .ToObservable();
        }

        #endregion

        #region Streaming

        public IObservable<Message> GetRealtimeMessages(string roomId)
        {
            string url = _baseStreamingApiAddress + $"rooms/{roomId}/chatMessages";

            return Observable.Using(() => HttpClient,
                client => client.GetInputStreamAsync(new Uri(url))
                    .AsTask()
                    .ToObservable()
                    .Select(x => Observable.FromAsync(() => StreamHelper.ReadStreamAsync(x.AsStreamForRead())).Repeat())
                    .Concat()
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(JsonConvert.DeserializeObject<Message>));
        }

        #endregion
    }
}
