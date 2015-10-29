using GitterSharp.Model;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace GitterSharp.Services
{
    public interface IReactiveGitterApiService
    {
        #region Properties

        /// <summary>
        /// Token used by the Gitter API to provide access to the entire API
        /// </summary>
        string Token { get; set; }

        #endregion

        #region User

        /// <summary>
        /// Returns the current user logged
        /// (https://developer.gitter.im/docs/user-resource#get-the-current-user)
        /// </summary>
        /// <returns></returns>
        IObservable<User> GetCurrentUserAsync();

        /// <summary>
        /// Returns a list of organizations of the current user logged
        /// (https://developer.gitter.im/docs/user-resource#orgs)
        /// </summary>
        /// <param name="userId">Id of the user currently logged</param>
        /// <returns></returns>
        IObservable<IEnumerable<Organization>> GetOrganizationsAsync(string userId);

        /// <summary>
        /// Returns a list of repositories of the current user logged
        /// (https://developer.gitter.im/docs/user-resource#repos)
        /// </summary>
        /// <param name="userId">Id of the user currently logged</param>
        /// <returns></returns>
        IObservable<IEnumerable<Repository>> GetRepositoriesAsync(string userId);

        #endregion

        #region Unread Items

        /// <summary>
        /// Retrieve unread chat messages of a specific room
        /// (https://developer.gitter.im/docs/user-resource#unread-items)
        /// </summary>
        /// <param name="userId">Id of the user who unread the messages</param>
        /// <param name="roomId">Id of the room that contains the messages</param>
        /// <returns></returns>
        IObservable<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId);

        /// <summary>
        /// Send a query that informs messages was read by the user
        /// (https://developer.gitter.im/docs/user-resource#mark-unread-items)
        /// </summary>
        /// <param name="userId">Id of the user who read the messages</param>
        /// <param name="roomId">Id of the room that contains the messages</param>
        /// <param name="messageIds">List of Id of messages read</param>
        /// <returns></returns>
        IObservable<Unit> MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds);

        #endregion

        #region Rooms

        /// <summary>
        /// Returns list of rooms of the user logged
        /// (https://developer.gitter.im/docs/rooms-resource#list-rooms)
        /// </summary>
        /// <returns></returns>
        IObservable<IEnumerable<Room>> GetRoomsAsync();

        /// <summary>
        /// Join and retrieve the room the user ask using the URI of the room
        /// (https://developer.gitter.im/docs/rooms-resource#join-a-room)
        /// </summary>
        /// <param name="roomName">Name of the room targeted (example: 'Odonno/Modern-Gitter')</param>
        /// <returns></returns>
        IObservable<Room> JoinRoomAsync(string roomName);

        #endregion

        #region Messages

        /// <summary>
        /// Retrieve a single message of a specific room
        /// (https://developer.gitter.im/docs/messages-resource#get-a-single-message)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <param name="messageId">Id of the message you are looking for</param>
        /// <returns></returns>
        IObservable<Message> GetSingleRoomMessageAsync(string roomId, string messageId);

        /// <summary>
        /// Retrieve messages of a specific room
        /// (https://developer.gitter.im/docs/messages-resource#list-messages)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <param name="limit">The limit of messages returned by the request</param>
        /// <param name="beforeId">Id of a message (used to truncate messages after this message id)</param>
        /// <param name="afterId">Id of a message (used to truncate messages before this message id)</param>
        /// <param name="skip">The number of messages to skip in the request</param>
        /// <returns></returns>
        IObservable<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, int limit = 50, string beforeId = null, string afterId = null, int skip = 0);

        /// <summary>
        /// Send a new message
        /// (https://developer.gitter.im/docs/messages-resource#send-a-message)
        /// </summary>
        /// <param name="roomId">Id of the room that will contain this message</param>
        /// <param name="message">Content of the message</param>
        /// <returns></returns>
        IObservable<Message> SendMessageAsync(string roomId, string message);

        /// <summary>
        /// Update an existing message
        /// (https://developer.gitter.im/docs/messages-resource#update-a-message)
        /// </summary>
        /// <param name="roomId">Id of the room that contains this message</param>
        /// <param name="messageId">Id of the existing message</param>
        /// <param name="message">Content of the message</param>
        /// <returns></returns>
        IObservable<Message> UpdateMessageAsync(string roomId, string messageId, string message);

        #endregion

        #region Streaming

        /// <summary>
        /// Retrieve messages of a specific room - in realtime
        /// (https://developer.gitter.im/docs/streaming-api)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <returns></returns>
        IObservable<Message> GetRealtimeMessages(string roomId);

        #endregion
    }
}
