using System;
using System.Collections.Generic;
using GitterSharp.Model;
using System.Reactive;
using GitterSharp.Configuration;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using GitterSharp.Helpers;
using GitterSharp.Model.Requests;
using GitterSharp.Model.Responses;
using System.Net.Http;
using System.Net.Http.Headers;

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

        #region Repository

        /// <summary>
        /// Returns the repository info from GitHub API
        /// </summary>
        /// <param name="repositoryName">Full name of the repository (ex: "owner/repoName")</param>
        /// <returns></returns>
        IObservable<RepositoryInfo> GetRepositoryInfo(string repositoryName);

        #endregion

        #region User

        /// <summary>
        /// Returns the current user logged
        /// (https://developer.gitter.im/docs/authentication#check-who-you-are-authenticated-as)
        /// </summary>
        /// <returns></returns>
        IObservable<GitterUser> GetCurrentUser();

        /// <summary>
        /// Returns a list of organizations of the current user
        /// </summary>
        /// <param name="unused">Only returns orgs that have no Gitter room</param>
        /// <returns></returns>
        IObservable<IEnumerable<Organization>> GetMyOrganizations(bool unused = false);

        /// <summary>
        /// Returns a list of organizations of a user
        /// (https://developer.gitter.im/docs/user-resource#orgs)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        IObservable<IEnumerable<Organization>> GetOrganizations(string userId);

        /// <summary>
        /// Returns a list of repositories of the current user (filtered by their name)
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="limit">Number max of results (0 = no limit)</param>
        /// <returns></returns>
        IObservable<IEnumerable<Repository>> GetMyRepositories(string query, int limit = 0);

        /// <summary>
        /// Returns a list of repositories of the current user
        /// </summary>
        /// <param name="unused">Only returns orgs that have no Gitter room</param>
        /// <returns></returns>
        IObservable<IEnumerable<Repository>> GetMyRepositories(bool unused = false);

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

        /// <summary>
        /// Returns an aggregation of count unread messages/mentions by room for the current user
        /// </summary>
        /// <returns></returns>
        IObservable<IEnumerable<RoomUnreadCount>> GetAggregatedUnreadItems();

        /// <summary>
        /// Retrieve user info by username
        /// </summary>
        /// <param name="username">Username of a user</param>
        /// <returns></returns>
        IObservable<UserInfo> GetUserInfo(string username);

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
        IObservable<IEnumerable<GitterUser>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0);  // TODO : `limit` and `skip` does not exist anymore

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
        /// Ban user from a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="username">Username of the user to ban</param>
        /// <param name="removeMessages">Removes all messages of the user in the room</param>
        /// <returns></returns>
        IObservable<BanUserResponse> BanUserFromRoom(string roomId, string username, bool removeMessages = false);

        /// <summary>
        /// Unban user from room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        IObservable<SuccessResponse> UnbanUser(string roomId, string userId);

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

        /// <summary>
        /// Invite a user in a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request info to invite a user</param>
        /// <returns></returns>
        IObservable<InviteUserResponse> InviteUserInRoom(string roomId, InviteUserRequest request);

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
        /// <param name="message">Content of the message (max length: 4096)</param>
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

        /// <summary>
        /// Remove a message from a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="messageId">Id of the message</param>
        /// <returns></returns>
        IObservable<Unit> DeleteMessage(string roomId, string messageId);

        /// <summary>
        /// Returns a list of users who already read the message
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="messageId">Id of the message</param>
        /// <returns></returns>
        IObservable<IEnumerable<GitterUser>> GetUsersWhoReadMessage(string roomId, string messageId);

        #endregion

        #region Events

        /// <summary>
        /// Returns list of room events
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="limit">The limit of users returned by the request</param>
        /// <param name="skip">The number of users to skip in the request</param>
        /// <param name="beforeId">Id of an event (used to truncate events after this event id)</param>
        /// <returns></returns>
        IObservable<IEnumerable<RoomEvent>> GetRoomEvents(string roomId, int limit = 50, int skip = 0, string beforeId = null);

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

        /// <summary>
        /// Get suggested rooms based on the group selected
        /// </summary>
        /// <param name="groupId">Id of the group</param>
        /// <returns></returns>
        IObservable<IEnumerable<Room>> GetSuggestedRoomsFromGroup(string groupId);

        #endregion

        #region Search

        /// <summary>
        /// Search rooms
        /// </summary>
        /// <param name="query">Query to search rooms (example: "gitter" for rooms relative to gitter)</param>
        /// <param name="limit">Number max of results</param>
        /// <param name="skip">The number of rooms to skip in the request</param>
        /// <returns></returns>
        IObservable<SearchResponse<Room>> SearchRooms(string query, int limit = 10, int skip = 0);

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="query">Query to search users</param>
        /// <param name="limit">Number max of results</param>
        /// <param name="skip">The number of users to skip in the request</param>
        /// <returns></returns>
        IObservable<SearchResponse<GitterUser>> SearchUsers(string query, int limit = 10, int skip = 0);

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

    public class ReactiveGitterApiService : IReactiveGitterApiService
    {
        #region Fields

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

        #region Repository

        public IObservable<RepositoryInfo> GetRepositoryInfo(string repositoryName)
        {
            return _apiService.GetRepositoryInfoAsync(repositoryName).ToObservable();
        }

        #endregion

        #region User

        public IObservable<GitterUser> GetCurrentUser()
        {
            return _apiService.GetCurrentUserAsync().ToObservable();
        }

        public IObservable<IEnumerable<Organization>> GetMyOrganizations(bool unused = false)
        {
            return _apiService.GetMyOrganizationsAsync(unused).ToObservable();
        }

        public IObservable<IEnumerable<Organization>> GetOrganizations(string userId)
        {
            return _apiService.GetOrganizationsAsync(userId).ToObservable();
        }

        public IObservable<IEnumerable<Repository>> GetMyRepositories(string query, int limit = 0)
        {
            return _apiService.GetMyRepositoriesAsync(query, limit).ToObservable();
        }

        public IObservable<IEnumerable<Repository>> GetMyRepositories(bool unused = false)
        {
            return _apiService.GetMyRepositoriesAsync(unused).ToObservable();
        }

        public IObservable<IEnumerable<Repository>> GetRepositories(string userId)
        {
            return _apiService.GetRepositoriesAsync(userId).ToObservable();
        }

        public IObservable<IEnumerable<Room>> GetSuggestedRooms()
        {
            return _apiService.GetSuggestedRoomsAsync().ToObservable();
        }

        public IObservable<IEnumerable<RoomUnreadCount>> GetAggregatedUnreadItems()
        {
            return _apiService.GetAggregatedUnreadItemsAsync().ToObservable();
        }

        public IObservable<UserInfo> GetUserInfo(string username)
        {
            return _apiService.GetUserInfoAsync(username).ToObservable();
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

        public IObservable<IEnumerable<GitterUser>> GetRoomUsers(string roomId, int limit = 30, string q = null, int skip = 0)
        {
            return _apiService.GetRoomUsersAsync(roomId, limit, q, skip).ToObservable();
        }

        public IObservable<Room> JoinRoom(string roomName)
        {
            return _apiService.JoinRoomAsync(roomName).ToObservable();
        }

        public IObservable<Room> JoinRoom(string userId, string roomId)
        {
            return _apiService.JoinRoomAsync(userId, roomId).ToObservable();
        }

        public IObservable<Room> UpdateRoom(string roomId, UpdateRoomRequest request)
        {
            return _apiService.UpdateRoomAsync(roomId, request).ToObservable();
        }

        public IObservable<bool> UpdateUserRoomSettings(string userId, string roomId, UpdateUserRoomSettingsRequest request)
        {
            return _apiService.UpdateUserRoomSettingsAsync(userId, roomId, request).ToObservable();
        }

        public IObservable<RoomNotificationSettingsResponse> GetRoomNotificationSettings(string userId, string roomId)
        {
            return _apiService.GetRoomNotificationSettingsAsync(userId, roomId).ToObservable();
        }

        public IObservable<RoomNotificationSettingsResponse> UpdateRoomNotificationSettings(string userId, string roomId, UpdateRoomNotificationSettingsRequest request)
        {
            return _apiService.UpdateRoomNotificationSettingsAsync(userId, roomId, request).ToObservable();
        }

        public IObservable<SuccessResponse> LeaveRoom(string roomId, string userId)
        {
            return _apiService.LeaveRoomAsync(roomId, userId).ToObservable();
        }

        public IObservable<SuccessResponse> DeleteRoom(string roomId)
        {
            return _apiService.DeleteRoomAsync(roomId).ToObservable();
        }

        public IObservable<IEnumerable<Room>> GetSuggestedRooms(string roomId)
        {
            return _apiService.GetSuggestedRoomsAsync(roomId).ToObservable();
        }

        public IObservable<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoom(string roomId)
        {
            return _apiService.GetSuggestedCollaboratorsOnRoomAsync(roomId).ToObservable();
        }

        public IObservable<IEnumerable<RoomIssue>> GetRoomIssues(string roomId)
        {
            return _apiService.GetRoomIssuesAsync(roomId).ToObservable();
        }

        public IObservable<IEnumerable<Ban>> GetRoomBans(string roomId)
        {
            return _apiService.GetRoomBansAsync(roomId).ToObservable();
        }

        public IObservable<BanUserResponse> BanUserFromRoom(string roomId, string username, bool removeMessages = false)
        {
            return _apiService.BanUserFromRoomAsync(roomId, username, removeMessages).ToObservable();
        }

        public IObservable<SuccessResponse> UnbanUser(string roomId, string userId)
        {
            return _apiService.UnbanUserAsync(roomId, userId).ToObservable();
        }

        public IObservable<WelcomeMessage> GetWelcomeMessage(string roomId)
        {
            return _apiService.GetWelcomeMessageAsync(roomId).ToObservable();
        }

        public IObservable<UpdateWelcomeMessageResponse> UpdateWelcomeMessage(string roomId, UpdateWelcomeMessageRequest request)
        {
            return _apiService.UpdateWelcomeMessageAsync(roomId, request).ToObservable();
        }

        public IObservable<InviteUserResponse> InviteUserInRoom(string roomId, InviteUserRequest request)
        {
            return _apiService.InviteUserInRoomAsync(roomId, request).ToObservable();
        }

        #endregion

        #region Messages

        public IObservable<Message> GetSingleRoomMessage(string roomId, string messageId)
        {
            return _apiService.GetSingleRoomMessageAsync(roomId, messageId).ToObservable();
        }

        public IObservable<IEnumerable<Message>> GetRoomMessages(string roomId, MessageRequest request)
        {
            return _apiService.GetRoomMessagesAsync(roomId, request).ToObservable();
        }

        public IObservable<Message> SendMessage(string roomId, string message)
        {
            return _apiService.SendMessageAsync(roomId, message).ToObservable();
        }

        public IObservable<Message> UpdateMessage(string roomId, string messageId, string message)
        {
            return _apiService.UpdateMessageAsync(roomId, messageId, message).ToObservable();
        }

        public IObservable<Unit> DeleteMessage(string roomId, string messageId)
        {
            return _apiService.DeleteMessageAsync(roomId, messageId).ToObservable();
        }

        public IObservable<IEnumerable<GitterUser>> GetUsersWhoReadMessage(string roomId, string messageId)
        {
            return _apiService.GetUsersWhoReadMessageAsync(roomId, messageId).ToObservable();
        }

        #endregion

        #region Events

        public IObservable<IEnumerable<RoomEvent>> GetRoomEvents(string roomId, int limit = 50, int skip = 0, string beforeId = null)
        {
            return _apiService.GetRoomEventsAsync(roomId, limit, skip, beforeId).ToObservable();
        }

        #endregion

        #region Groups

        public IObservable<IEnumerable<Group>> GetGroups()
        {
            return _apiService.GetGroupsAsync().ToObservable();
        }

        public IObservable<IEnumerable<Room>> GetGroupRooms(string groupId)
        {
            return _apiService.GetGroupRoomsAsync(groupId).ToObservable();
        }

        public IObservable<Room> CreateRoom(string groupId, CreateRoomRequest request)
        {
            return _apiService.CreateRoomAsync(groupId, request).ToObservable();
        }

        public IObservable<IEnumerable<Room>> GetSuggestedRoomsFromGroup(string groupId)
        {
            return _apiService.GetSuggestedRoomsFromGroupAsync(groupId).ToObservable();
        }

        #endregion

        #region Search

        public IObservable<SearchResponse<Room>> SearchRooms(string query, int limit = 10, int skip = 0)
        {
            return _apiService.SearchRoomsAsync(query, limit, skip).ToObservable();
        }

        public IObservable<SearchResponse<GitterUser>> SearchUsers(string query, int limit = 10, int skip = 0)
        {
            return _apiService.SearchUsersAsync(query, limit, skip).ToObservable();
        }

        public IObservable<SearchResponse<Repository>> SearchUserRepositories(string userId, string query, int limit = 10)
        {
            return _apiService.SearchUserRepositoriesAsync(userId, query, limit).ToObservable();
        }

        #endregion

        #region Streaming

        public IObservable<Message> GetRealtimeMessages(string roomId)
        {
            string url = _baseStreamingApiAddress + $"rooms/{roomId}/chatMessages";
            return HttpClient.CreateObservableHttpStream<Message>(url);
        }

        public IObservable<RoomEvent> GetRealtimeEvents(string roomId)
        {
            string url = _baseStreamingApiAddress + $"rooms/{roomId}/events";
            return HttpClient.CreateObservableHttpStream<RoomEvent>(url);
        }

        #endregion

        #region Analytics

        public IObservable<Dictionary<DateTime, int>> GetRoomMessagesCountByDay(string roomId)
        {
            return _apiService.GetRoomMessagesCountByDayAsync(roomId).ToObservable();
        }

        #endregion
    }
}