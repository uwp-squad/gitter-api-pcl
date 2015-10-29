using GitterSharp.Services;
using System;
using System.Collections.Generic;
using GitterSharp.Model;
using System.Reactive;

namespace GitterSharp.UniversalWindows.Services
{
    public class ReactiveGitterApiService : IReactiveGitterApiService
    {
        #region Properties

        public string Token { get; set; }

        #endregion

        #region User

        public IObservable<User> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public IObservable<IEnumerable<Organization>> GetOrganizationsAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public IObservable<IEnumerable<Repository>> GetRepositoriesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Unread Items

        public IObservable<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Rooms

        public IObservable<IEnumerable<Room>> GetRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public IObservable<Room> JoinRoomAsync(string roomName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Messages

        public IObservable<Message> GetSingleRoomMessageAsync(string roomId, string messageId)
        {
            throw new NotImplementedException();
        }

        public IObservable<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0)
        {
            throw new NotImplementedException();
        }

        public IObservable<Message> SendMessageAsync(string roomId, string message)
        {
            throw new NotImplementedException();
        }

        public IObservable<Message> UpdateMessageAsync(string roomId, string messageId, string message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Streaming

        public IObservable<Message> GetRealtimeMessages(string roomId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
