﻿using System;
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

        private IGitterApiService _apiService = new GitterApiService();

        #endregion

        #region Properties

        public string Token
        {
            get { return _apiService.Token; }
            set { _apiService.Token = value; }
        }

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
            return _apiService.GetCurrentUserAsync().ToObservable();
        }

        public IObservable<IEnumerable<Organization>> GetOrganizations(string userId)
        {
            return _apiService.GetOrganizationsAsync(userId).ToObservable();
        }

        public IObservable<IEnumerable<Repository>> GetRepositories(string userId)
        {
            return _apiService.GetRepositoriesAsync(userId).ToObservable();
        }

        #endregion

        #region Unread Items

        public IObservable<UnreadItems> RetrieveUnreadChatMessages(string userId, string roomId)
        {
            return _apiService.RetrieveUnreadChatMessagesAsync(userId, roomId).ToObservable();
        }

        public IObservable<Unit> MarkUnreadChatMessages(string userId, string roomId, IEnumerable<string> messageIds)
        {
            return _apiService.MarkUnreadChatMessagesAsync(userId, roomId, messageIds).ToObservable();
        }

        #endregion

        #region Rooms

        public IObservable<IEnumerable<Room>> GetRooms()
        {
            return _apiService.GetRoomsAsync().ToObservable();
        }

        public IObservable<IEnumerable<User>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0)
        {
            return _apiService.GetRoomUsersAsync(roomId, limit, q, skip).ToObservable();
        }

        public IObservable<Room> JoinRoom(string roomName)
        {
            return _apiService.JoinRoomAsync(roomName).ToObservable();
        }

        #endregion

        #region Messages

        public IObservable<Message> GetSingleRoomMessage(string roomId, string messageId)
        {
            return _apiService.GetSingleRoomMessageAsync(roomId, messageId).ToObservable();
        }

        public IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0)
        {
            return _apiService.GetRoomMessagesAsync(roomId, limit, beforeId, afterId, skip).ToObservable();
        }

        public IObservable<Message> SendMessage(string roomId, string message)
        {
            return _apiService.SendMessageAsync(roomId, message).ToObservable();
        }

        public IObservable<Message> UpdateMessage(string roomId, string messageId, string message)
        {
            return _apiService.UpdateMessageAsync(roomId, messageId, message).ToObservable();
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