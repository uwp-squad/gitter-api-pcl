using GitterSharp.Model;
using GitterSharp.Model.Requests;
using GitterSharp.Model.Responses;
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
        /// (https://developer.gitter.im/docs/authentication#check-who-you-are-authenticated-as)
        /// </summary>
        /// <returns></returns>
        IObservable<User> GetCurrentUser();

        /// <summary>
        /// Returns a list of organizations of a user
        /// (https://developer.gitter.im/docs/user-resource#orgs)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        IObservable<IEnumerable<Organization>> GetOrganizations(string userId);

        /// <summary>
        /// Returns a list of repositories of a user
        /// (https://developer.gitter.im/docs/user-resource#repos)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        IObservable<IEnumerable<Repository>> GetRepositories(string userId);

        /// <summary>
        /// Returns list of suggested rooms for the current user
        /// </summary>
        /// <returns></returns>
        IObservable<IEnumerable<Room>> GetSuggestedRooms();

        #endregion

        #region Unread Items

        /// <summary>
        /// Retrieve unread chat messages of a specific room
        /// (https://developer.gitter.im/docs/user-resource#unread-items)
        /// </summary>
        /// <param name="userId">Id of the user who unread the messages</param>
        /// <param name="roomId">Id of the room that contains the messages</param>
        /// <returns></returns>
        IObservable<UnreadItems> RetrieveUnreadChatMessages(string userId, string roomId);

        /// <summary>
        /// Send a query that informs messages was read by the user
        /// (https://developer.gitter.im/docs/user-resource#mark-unread-items)
        /// </summary>
        /// <param name="userId">Id of the user who read the messages</param>
        /// <param name="roomId">Id of the room that contains the messages</param>
        /// <param name="messageIds">List of Id of messages read</param>
        /// <returns></returns>
        IObservable<Unit> MarkUnreadChatMessages(string userId, string roomId, IEnumerable<string> messageIds);

        #endregion

        #region Rooms

        /// <summary>
        /// Returns list of rooms of the user logged
        /// (https://developer.gitter.im/docs/rooms-resource#list-rooms)
        /// </summary>
        /// <returns></returns>
        IObservable<IEnumerable<Room>> GetRooms();

        /// <summary>
        /// Returns list of users in the room
        /// (https://developer.gitter.im/docs/rooms-resource#users)
        /// </summary>
        /// <param name="roomId">Id of the room to get user list from</param>
        /// <param name="limit">The limit of users returned by the request</param>
        /// <param name="q">A search query for user names</param>
        /// <param name="skip">The number of users to skip in the request</param>
        /// <returns></returns>
        IObservable<IEnumerable<User>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0);  // TODO : `limit` and `skip` does not exist anymore

        /// <summary>
        /// Join and retrieve the room the user ask using the URI of the room
        /// (https://developer.gitter.im/docs/rooms-resource#join-a-room)
        /// </summary>
        /// <param name="roomName">Name of the room targeted (example: 'Odonno/Modern-Gitter')</param>
        /// <returns></returns>
        IObservable<Room> JoinRoom(string roomName);

        /// <summary>
        /// Join the room using room id
        /// </summary>
        /// <param name="userId">Id of the user (TBD)</param>
        /// <param name="roomId">Id of the room the user want to join</param>
        /// <returns></returns>
        IObservable<Room> JoinRoom(string userId, string roomId);

        /// <summary>
        /// Update room information
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request for setting room information</param>
        /// <returns></returns>
        IObservable<Room> UpdateRoom(string roomId, UpdateRoomRequest request);

        /// <summary>
        /// Update room settings for the user
        /// </summary>
        /// <param name="userId">Id of the user (generally current user)</param>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request setting user room settings</param>
        /// <returns></returns>
        IObservable<bool> UpdateUserRoomSettings(string userId, string roomId, UpdateUserRoomSettingsRequest request);

        /// <summary>
        /// Get notification room settings of a room (for the user)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<RoomNotificationSettingsResponse> GetRoomNotificationSettings(string userId, string roomId);

        /// <summary>
        /// Update notification room settings of a room (for the user)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request setting notification room settings</param>
        /// <returns></returns>
        IObservable<RoomNotificationSettingsResponse> UpdateRoomNotificationSettings(string userId, string roomId, UpdateRoomNotificationSettingsRequest request);

        /// <summary>
        /// Leave the room
        /// If it is the current user, leave the room
        /// If it is another user, remove user from the room if we have admin rights
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        IObservable<SuccessResponse> LeaveRoom(string roomId, string userId);

        /// <summary>
        /// Delete room by its id
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<SuccessResponse> DeleteRoom(string roomId);

        /// <summary>
        /// Returns list of suggested rooms, based on your current room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<IEnumerable<Room>> GetSuggestedRooms(string roomId);

        /// <summary>
        /// Returns list of possible collaborators to invite on a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoom(string roomId);

        /// <summary>
        /// Returns list of room issues
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<IEnumerable<RoomIssue>> GetRoomIssues(string roomId);

        /// <summary>
        /// Returns list of bans (user banned) of a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<IEnumerable<Ban>> GetRoomBans(string roomId);

        /// <summary>
        /// Returns welcome message of a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<WelcomeMessage> GetWelcomeMessage(string roomId);

        /// <summary>
        /// Update welcome message of room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request to edit room welcome message</param>
        /// <returns></returns>
        IObservable<UpdateWelcomeMessageResponse> UpdateWelcomeMessage(string roomId, UpdateWelcomeMessageRequest request);

        #endregion

        #region Messages

        /// <summary>
        /// Retrieve a single message of a specific room
        /// (https://developer.gitter.im/docs/messages-resource#get-a-single-message)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <param name="messageId">Id of the message you are looking for</param>
        /// <returns></returns>
        IObservable<Message> GetSingleRoomMessage(string roomId, string messageId);

        /// <summary>
        /// Retrieve messages of a specific room
        /// (https://developer.gitter.im/docs/messages-resource#list-messages)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <param name="request">Request for search and navigation</param>
        /// <returns></returns>
        IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, MessageRequest request);

        /// <summary>
        /// Send a new message
        /// (https://developer.gitter.im/docs/messages-resource#send-a-message)
        /// </summary>
        /// <param name="roomId">Id of the room that will contain this message</param>
        /// <param name="message">Content of the message</param>
        /// <returns></returns>
        IObservable<Message> SendMessage(string roomId, string message);

        /// <summary>
        /// Update an existing message
        /// (https://developer.gitter.im/docs/messages-resource#update-a-message)
        /// </summary>
        /// <param name="roomId">Id of the room that contains this message</param>
        /// <param name="messageId">Id of the existing message</param>
        /// <param name="message">Content of the message</param>
        /// <returns></returns>
        IObservable<Message> UpdateMessage(string roomId, string messageId, string message);

        #endregion

        #region Events

        /// <summary>
        /// Returns list of room events
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<IEnumerable<RoomEvent>> GetRoomEvents(string roomId);

        #endregion

        #region Groups

        /// <summary>
        /// Returns list of groups the user is currently in
        /// (https://developer.gitter.im/docs/groups-resource#list-groups)
        /// </summary>
        /// <returns></returns>
        IObservable<IEnumerable<Group>> GetGroups();

        /// <summary>
        /// Returns list of rooms inside the group
        /// (https://developer.gitter.im/docs/groups-resource#list-rooms-under-group)
        /// </summary>
        /// <param name="groupId">Id of the group</param>
        /// <returns></returns>
        IObservable<IEnumerable<Room>> GetGroupRooms(string groupId);

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="groupId">Id of the group</param>
        /// <param name="request">Request to create the room</param>
        /// <returns></returns>
        IObservable<Room> CreateRoom(string groupId, CreateRoomRequest request);

        #endregion

        #region Search

        /// <summary>
        /// Search rooms
        /// </summary>
        /// <param name="query">Query to search rooms (example: "gitter" for rooms relative to gitter)</param>
        /// <param name="limit">Number max of results</param>
        /// <returns></returns>
        IObservable<SearchResponse<Room>> SearchRooms(string query, int limit = 10);

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="query">Query to search users</param>
        /// <param name="limit">Number max of results</param>
        /// <returns></returns>
        IObservable<SearchResponse<User>> SearchUsers(string query, int limit = 10);

        /// <summary>
        /// Search repositories of a user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="query">Query to search repositories</param>
        /// <param name="limit">Number max of results</param>
        /// <returns></returns>
        IObservable<SearchResponse<Repository>> SearchUserRepositories(string userId, string query, int limit = 10);

        #endregion

        #region Streaming

        /// <summary>
        /// Retrieve messages of a specific room - in realtime
        /// (https://developer.gitter.im/docs/streaming-api)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <returns></returns>
        IObservable<Message> GetRealtimeMessages(string roomId);

        /// <summary>
        /// Retrieve events of a specific room - in realtime
        /// (https://developer.gitter.im/docs/streaming-api)
        /// </summary>
        /// <param name="roomId">Id of the room that contains events (activity tab)</param>
        /// <returns></returns>
        IObservable<RoomEvent> GetRealtimeEvents(string roomId);

        #endregion

        #region Analytics

        /// <summary>
        /// Retrieve all messages count of a room, grouped by day
        /// Warning ! It only returns messages count from a year ago
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        IObservable<Dictionary<DateTime, int>> GetRoomMessagesCountByDay(string roomId);

        #endregion
    }
}
