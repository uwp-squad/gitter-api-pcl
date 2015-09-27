using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gitter.Model;

namespace Gitter.API.Services.Abstract
{
    public interface IGitterApiService
    {
        #region Authentication
        
        void TryAuthenticate(string token);

        #endregion

        #region User

        Task<User> GetCurrentUserAsync();
        Task ReadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds);

        #endregion

        #region Rooms

        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room> JoinRoomAsync(string uri);

        #endregion

        #region Messages

        IObservable<Message> GetRealtimeMessages(string roomId);
        Task<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0);
        Task<Message> SendMessageAsync(string roomId, string message);
        Task<Message> UpdateMessageAsync(string roomId, string messageId, string message);

        #endregion
    }
}
