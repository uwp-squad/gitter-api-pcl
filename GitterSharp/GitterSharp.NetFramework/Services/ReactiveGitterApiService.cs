using System;
using System.Linq;
using System.Collections.Generic;
using GitterSharp.Model;
using System.Reactive;
using GitterSharp.Configuration;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Newtonsoft.Json;
using GitterSharp.Helpers;
#if __IOS__ || __ANDROID__ || NET45
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
#endif
#if NETFX_CORE
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using System.IO;
using Windows.Storage.Streams;
#endif

namespace GitterSharp.Services
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

#if __IOS__ || __ANDROID__ || NET45
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
#endif
#if NETFX_CORE
                httpClient.DefaultRequestHeaders.Accept.Add(new HttpMediaTypeWithQualityHeaderValue("application/json"));
                if (!string.IsNullOrWhiteSpace(Token))
                    httpClient.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", Token);
#endif

                return httpClient;
            }
        }

        #endregion

        #region Properties

        public string Token { get; set; }

        #endregion

        #region Constructors

        public ReactiveGitterApiService() { }

        public ReactiveGitterApiService(string token)
        {
            Token = token;
        }

        #endregion

        #region User

        public IObservable<User> GetCurrentUser()
        {
            string url = _baseApiAddress + "user";
            return HttpClient.GetAsync<IEnumerable<User>>(url)
                .ToObservable()
                .Select(users => users.FirstOrDefault());
        }

        public IObservable<IEnumerable<Organization>> GetOrganizations(string userId)
        {
            string url = _baseApiAddress + $"user/{userId}/orgs";
            return HttpClient.GetAsync<IEnumerable<Organization>>(url)
                .ToObservable();
        }

        public IObservable<IEnumerable<Repository>> GetRepositories(string userId)
        {
            string url = _baseApiAddress + $"user/{userId}/repos";
            return HttpClient.GetAsync<IEnumerable<Repository>>(url)
                .ToObservable();
        }

        #endregion

        #region Unread Items

        public IObservable<UnreadItems> RetrieveUnreadChatMessages(string userId, string roomId)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/unreadItems";
            return HttpClient.GetAsync<UnreadItems>(url)
                .ToObservable();
        }

        public IObservable<Unit> MarkUnreadChatMessages(string userId, string roomId, IEnumerable<string> messageIds)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/unreadItems";

#if __IOS__ || __ANDROID__ || NET45
            var content = new StringContent("{\"chat\": " + JsonConvert.SerializeObject(messageIds) + "}",
                Encoding.UTF8,
                "application/json");
#endif
#if NETFX_CORE
            var content = new HttpStringContent("{\"chat\": " + JsonConvert.SerializeObject(messageIds) + "}",
                UnicodeEncoding.Utf8,
                "application/json");
#endif

            return HttpClient.PostAsync(url, content)
                .ToObservable()
                .Select(x => new Unit());
        }

        #endregion

        #region Rooms

        public IObservable<IEnumerable<Room>> GetRooms()
        {
            string url = _baseApiAddress + "rooms";
            return HttpClient.GetAsync<IEnumerable<Room>>(url)
                .ToObservable();
        }

        public IObservable<IEnumerable<User>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/users?limit={limit}";

            if (!string.IsNullOrWhiteSpace(q))
                url += $"&q={q}";

            if (skip > 0)
                url += $"&skip={skip}";

            return HttpClient.GetAsync<IEnumerable<User>>(url)
                .ToObservable();
        }

        public IObservable<Room> JoinRoom(string roomName)
        {
            string url = _baseApiAddress + "rooms";

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"uri", roomName}
            });
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                {"uri", roomName}
            });
#endif

            return HttpClient.PostAsync<Room>(url, content)
                .ToObservable();
        }

        #endregion

        #region Messages

        public IObservable<Message> GetSingleRoomMessage(string roomId, string messageId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}";
            return HttpClient.GetAsync<Message>(url)
                .ToObservable();
        }

        public IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0)
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

        public IObservable<Message> SendMessage(string roomId, string message)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages";

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });
#endif

            return HttpClient.PostAsync<Message>(url, content)
                .ToObservable();
        }

        public IObservable<Message> UpdateMessage(string roomId, string messageId, string message)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}";

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });
#endif

            return HttpClient.PutAsync<Message>(url, content)
                .ToObservable();
        }

        #endregion

        #region Streaming

        public IObservable<Message> GetRealtimeMessages(string roomId)
        {
            string url = _baseStreamingApiAddress + $"rooms/{roomId}/chatMessages";

#if __IOS__ || __ANDROID__ || NET45
            return Observable.Using(() => HttpClient,
                client => client.GetStreamAsync(new Uri(url))
                    .ToObservable()
                    .Select(x => Observable.FromAsync(() => StreamHelper.ReadStreamAsync(x)).Repeat())
                    .Concat()
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(JsonConvert.DeserializeObject<Message>));
#endif
#if NETFX_CORE
            return Observable.Using(() => HttpClient,
                client => client.GetInputStreamAsync(new Uri(url))
                    .AsTask()
                    .ToObservable()
                    .Select(x => Observable.FromAsync(() => StreamHelper.ReadStreamAsync(x.AsStreamForRead())).Repeat())
                    .Concat()
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(JsonConvert.DeserializeObject<Message>));
#endif
        }

        #endregion
    }
}