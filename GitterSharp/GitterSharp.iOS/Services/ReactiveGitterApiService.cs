using GitterSharp.Services;
using System;
using System.Collections.Generic;
using GitterSharp.Model;
using System.Reactive;
using System.Net.Http;
using System.Net.Http.Headers;
using GitterSharp.Configuration;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Newtonsoft.Json;
using GitterSharp.Helpers;

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

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (!string.IsNullOrWhiteSpace(Token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

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
            throw new NotImplementedException();
        }

        public IObservable<IEnumerable<Organization>> GetOrganizations(string userId)
        {
            throw new NotImplementedException();
        }

        public IObservable<IEnumerable<Repository>> GetRepositories(string userId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Unread Items

        public IObservable<UnreadItems> RetrieveUnreadChatMessages(string userId, string roomId)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> MarkUnreadChatMessages(string userId, string roomId, IEnumerable<string> messageIds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Rooms

        public IObservable<IEnumerable<Room>> GetRooms()
        {
            throw new NotImplementedException();
        }

        public IObservable<Room> JoinRoom(string roomName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Messages

        public IObservable<Message> GetSingleRoomMessage(string roomId, string messageId)
        {
            throw new NotImplementedException();
        }

        public IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0)
        {
            throw new NotImplementedException();
        }

        public IObservable<Message> SendMessage(string roomId, string message)
        {
            throw new NotImplementedException();
        }

        public IObservable<Message> UpdateMessage(string roomId, string messageId, string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Streaming

        public IObservable<Message> GetRealtimeMessages(string roomId)
        {
            string url = _baseStreamingApiAddress + $"rooms/{roomId}/chatMessages";

            return Observable.Using(() => HttpClient,
                client => client.GetStreamAsync(new Uri(url))
                    .ToObservable()
                    .Select(x => Observable.FromAsync(() => StreamHelper.ReadStreamAsync(x)).Repeat())
                    .Concat()
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(JsonConvert.DeserializeObject<Message>));
        }

        #endregion
    }
}
