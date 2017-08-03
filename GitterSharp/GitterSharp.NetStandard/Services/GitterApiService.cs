using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GitterSharp.Configuration;
using GitterSharp.Helpers;
using GitterSharp.Model;
using Newtonsoft.Json;
using GitterSharp.Model.Requests;
using GitterSharp.Model.Responses;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace GitterSharp.Services
{
    public interface IGitterApiService
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
        Task<RepositoryInfo> GetRepositoryInfoAsync(string repositoryName);

        #endregion

        #region User

        /// <summary>
        /// Returns the current user logged
        /// (https://developer.gitter.im/docs/authentication#check-who-you-are-authenticated-as)
        /// </summary>
        /// <returns></returns>
        Task<GitterUser> GetCurrentUserAsync();

        /// <summary>
        /// Returns a list of organizations of the current user
        /// </summary>
        /// <param name="unused">Only returns orgs that have no Gitter room</param>
        /// <returns></returns>
        Task<IEnumerable<Organization>> GetMyOrganizationsAsync(bool unused = false);

        /// <summary>
        /// Returns a list of organizations of a user
        /// (https://developer.gitter.im/docs/user-resource#orgs)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<IEnumerable<Organization>> GetOrganizationsAsync(string userId);

        /// <summary>
        /// Returns a list of repositories of the current user (filtered by their name)
        /// </summary>
        /// <param name="query">Query string</param>
        /// <param name="limit">Number max of results (0 = no limit)</param>
        /// <returns></returns>
        Task<IEnumerable<Repository>> GetMyRepositoriesAsync(string query, int limit = 0);

        /// <summary>
        /// Returns a list of repositories of the current user
        /// </summary>
        /// <param name="unused">Only returns orgs that have no Gitter room</param>
        /// <returns></returns>
        Task<IEnumerable<Repository>> GetMyRepositoriesAsync(bool unused = false);

        /// <summary>
        /// Returns a list of repositories of a user
        /// (https://developer.gitter.im/docs/user-resource#repos)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<IEnumerable<Repository>> GetRepositoriesAsync(string userId);

        /// <summary>
        /// Returns list of suggested rooms for the current user
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetSuggestedRoomsAsync();

        /// <summary>
        /// Returns an aggregation of count unread messages/mentions by room for the current user
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<RoomUnreadCount>> GetAggregatedUnreadItemsAsync();

        /// <summary>
        /// Retrieve user info by username
        /// </summary>
        /// <param name="username">Username of a user</param>
        /// <returns></returns>
        Task<UserInfo> GetUserInfoAsync(string username);

        #endregion

        #region Unread Items

        /// <summary>
        /// Retrieve unread chat messages of a specific room
        /// (https://developer.gitter.im/docs/user-resource#unread-items)
        /// </summary>
        /// <param name="userId">Id of the user who unread the messages</param>
        /// <param name="roomId">Id of the room that contains the messages</param>
        /// <returns></returns>
        Task<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId);

        /// <summary>
        /// Send a query that informs messages was read by the user
        /// (https://developer.gitter.im/docs/user-resource#mark-unread-items)
        /// </summary>
        /// <param name="userId">Id of the user who read the messages</param>
        /// <param name="roomId">Id of the room that contains the messages</param>
        /// <param name="messageIds">List of Id of messages read</param>
        /// <returns></returns>
        Task MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds);

        #endregion

        #region Rooms

        /// <summary>
        /// Returns list of rooms of the user logged
        /// (https://developer.gitter.im/docs/rooms-resource#list-rooms)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetRoomsAsync();

        /// <summary>
        /// Returns list of users in the room
        /// (https://developer.gitter.im/docs/rooms-resource#users)
        /// </summary>
        /// <param name="roomId">Id of the room to get user list from</param>
        /// <param name="limit">The limit of users returned by the request</param>
        /// <param name="q">A search query for user names</param>
        /// <param name="skip">The number of users to skip in the request</param>
        /// <returns></returns>
        Task<IEnumerable<GitterUser>> GetRoomUsersAsync(string roomId, int limit = 30, string q = null, int skip = 0); // TODO : `limit` and `skip` does not exist anymore

        /// <summary>
        /// Join and retrieve the room the user ask using the URI of the room
        /// (https://developer.gitter.im/docs/rooms-resource#join-a-room)
        /// </summary>
        /// <param name="roomName">Name of the room targeted (example: 'Odonno/Modern-Gitter')</param>
        /// <returns></returns>
        Task<Room> JoinRoomAsync(string roomName);

        /// <summary>
        /// Join the room using room id
        /// </summary>
        /// <param name="userId">Id of the user (TBD)</param>
        /// <param name="roomId">Id of the room the user want to join</param>
        /// <returns></returns>
        Task<Room> JoinRoomAsync(string userId, string roomId);

        /// <summary>
        /// Update room information
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request for setting room information</param>
        /// <returns></returns>
        Task<Room> UpdateRoomAsync(string roomId, UpdateRoomRequest request);

        /// <summary>
        /// Update room settings for the user
        /// </summary>
        /// <param name="userId">Id of the user (generally current user)</param>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request setting user room settings</param>
        /// <returns></returns>
        Task<bool> UpdateUserRoomSettingsAsync(string userId, string roomId, UpdateUserRoomSettingsRequest request);

        /// <summary>
        /// Get notification room settings of a room (for the user)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<RoomNotificationSettingsResponse> GetRoomNotificationSettingsAsync(string userId, string roomId);

        /// <summary>
        /// Update notification room settings of a room (for the user)
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request setting notification room settings</param>
        /// <returns></returns>
        Task<RoomNotificationSettingsResponse> UpdateRoomNotificationSettingsAsync(string userId, string roomId, UpdateRoomNotificationSettingsRequest request);

        /// <summary>
        /// Leave the room
        /// If it is the current user, leave the room
        /// If it is another user, remove user from the room if we have admin rights
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<SuccessResponse> LeaveRoomAsync(string roomId, string userId);

        /// <summary>
        /// Delete room by its id
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<SuccessResponse> DeleteRoomAsync(string roomId);

        /// <summary>
        /// Returns list of suggested rooms, based on your current room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetSuggestedRoomsAsync(string roomId);

        /// <summary>
        /// Returns list of possible collaborators to invite on a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoomAsync(string roomId);

        /// <summary>
        /// Returns list of room issues
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<IEnumerable<RoomIssue>> GetRoomIssuesAsync(string roomId);

        /// <summary>
        /// Returns list of bans (user banned) of a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<IEnumerable<Ban>> GetRoomBansAsync(string roomId);

        /// <summary>
        /// Ban user from a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="username">Username of the user to ban</param>
        /// <param name="removeMessages">Removes all messages of the user in the room</param>
        /// <returns></returns>
        Task<BanUserResponse> BanUserFromRoomAsync(string roomId, string username, bool removeMessages = false);

        /// <summary>
        /// Unban user from room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="userId">Id of the user</param>
        /// <returns></returns>
        Task<SuccessResponse> UnbanUserAsync(string roomId, string userId);

        /// <summary>
        /// Returns welcome message of a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<WelcomeMessage> GetWelcomeMessageAsync(string roomId);

        /// <summary>
        /// Update welcome message of room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request to edit room welcome message</param>
        /// <returns></returns>
        Task<UpdateWelcomeMessageResponse> UpdateWelcomeMessageAsync(string roomId, UpdateWelcomeMessageRequest request);

        /// <summary>
        /// Invite a user in a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="request">Request info to invite a user</param>
        /// <returns></returns>
        Task<InviteUserResponse> InviteUserInRoomAsync(string roomId, InviteUserRequest request); 

        #endregion

        #region Messages

        /// <summary>
        /// Retrieve a single message of a specific room
        /// (https://developer.gitter.im/docs/messages-resource#get-a-single-message)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <param name="messageId">Id of the message you are looking for</param>
        /// <returns></returns>
        Task<Message> GetSingleRoomMessageAsync(string roomId, string messageId);

        /// <summary>
        /// Retrieve messages of a specific room
        /// (https://developer.gitter.im/docs/messages-resource#list-messages)
        /// </summary>
        /// <param name="roomId">Id of the room that contains messages</param>
        /// <param name="request">Request for search and navigation</param>
        /// <returns></returns>
        Task<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, MessageRequest request);

        /// <summary>
        /// Send a new message
        /// (https://developer.gitter.im/docs/messages-resource#send-a-message)
        /// </summary>
        /// <param name="roomId">Id of the room that will contain this message</param>
        /// <param name="message">Content of the message (max length: 4096)</param>
        /// <returns></returns>
        Task<Message> SendMessageAsync(string roomId, string message);

        /// <summary>
        /// Update an existing message
        /// (https://developer.gitter.im/docs/messages-resource#update-a-message)
        /// </summary>
        /// <param name="roomId">Id of the room that contains this message</param>
        /// <param name="messageId">Id of the existing message</param>
        /// <param name="message">Content of the message</param>
        /// <returns></returns>
        Task<Message> UpdateMessageAsync(string roomId, string messageId, string message);

        /// <summary>
        /// Remove a message from a room
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="messageId">Id of the message</param>
        /// <returns></returns>
        Task DeleteMessageAsync(string roomId, string messageId);

        /// <summary>
        /// Returns a list of users who already read the message
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <param name="messageId">Id of the message</param>
        /// <returns></returns>
        Task<IEnumerable<GitterUser>> GetUsersWhoReadMessageAsync(string roomId, string messageId);

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
        Task<IEnumerable<RoomEvent>> GetRoomEventsAsync(string roomId, int limit = 50, int skip = 0, string beforeId = null);

        #endregion

        #region Groups

        /// <summary>
        /// Returns list of groups the user is currently in
        /// (https://developer.gitter.im/docs/groups-resource#list-groups)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Group>> GetGroupsAsync();

        /// <summary>
        /// Returns list of rooms inside the group
        /// (https://developer.gitter.im/docs/groups-resource#list-rooms-under-group)
        /// </summary>
        /// <param name="groupId">Id of the group</param>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetGroupRoomsAsync(string groupId);

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="groupId">Id of the group</param>
        /// <param name="request">Request to create the room</param>
        /// <returns></returns>
        Task<Room> CreateRoomAsync(string groupId, CreateRoomRequest request);

        /// <summary>
        /// Get suggested rooms based on the group selected
        /// </summary>
        /// <param name="groupId">Id of the group</param>
        /// <returns></returns>
        Task<IEnumerable<Room>> GetSuggestedRoomsFromGroupAsync(string groupId);

        #endregion

        #region Search

        /// <summary>
        /// Search rooms
        /// </summary>
        /// <param name="query">Query to search rooms (example: "gitter" for rooms relative to gitter)</param>
        /// <param name="limit">Number max of results</param>
        /// <param name="skip">The number of rooms to skip in the request</param>
        /// <returns></returns>
        Task<SearchResponse<Room>> SearchRoomsAsync(string query, int limit = 10, int skip = 0);

        /// <summary>
        /// Search users
        /// </summary>
        /// <param name="query">Query to search users</param>
        /// <param name="limit">Number max of results</param>
        /// <param name="skip">The number of users to skip in the request</param>
        /// <returns></returns>
        Task<SearchResponse<GitterUser>> SearchUsersAsync(string query, int limit = 10, int skip = 0);

        /// <summary>
        /// Search repositories of a user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="query">Query to search repositories</param>
        /// <param name="limit">Number max of results</param>
        /// <returns></returns>
        Task<SearchResponse<Repository>> SearchUserRepositoriesAsync(string userId, string query, int limit = 10);

        #endregion

        #region Analytics

        /// <summary>
        /// Retrieve all messages count of a room, grouped by day
        /// Warning ! It only returns messages count from a year ago
        /// </summary>
        /// <param name="roomId">Id of the room</param>
        /// <returns></returns>
        Task<Dictionary<DateTime, int>> GetRoomMessagesCountByDayAsync(string roomId);

        #endregion
    }

    public class GitterApiService : IGitterApiService
    {
        #region Fields

        private readonly string _baseApiAddress = $"{Constants.ApiBaseUrl}{Constants.ApiVersion}";

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

        public GitterApiService() { }

        public GitterApiService(string token)
        {
            Token = token;
        }

        #endregion

        #region Repository

        public async Task<RepositoryInfo> GetRepositoryInfoAsync(string repositoryName)
        {
            if (string.IsNullOrWhiteSpace(repositoryName))
            {
                throw new ArgumentNullException(nameof(repositoryName));
            }

            string url = _baseApiAddress + $"repo-info?repo={repositoryName}";
            return await HttpClient.GetAsync<RepositoryInfo>(url);
        }

        #endregion

        #region User

        public async Task<GitterUser> GetCurrentUserAsync()
        {
            string url = _baseApiAddress + "user/me";
            return await HttpClient.GetAsync<GitterUser>(url);
        }

        public async Task<IEnumerable<Organization>> GetMyOrganizationsAsync(bool unused = false)
        {
            string url = _baseApiAddress + "user/me/orgs";

            if (unused)
                url += $"?type=unused";

            return await HttpClient.GetAsync<IEnumerable<Organization>>(url);
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync(string userId)
        {
            string url = _baseApiAddress + $"user/{userId}/orgs";
            return await HttpClient.GetAsync<IEnumerable<Organization>>(url);
        }

        public async Task<IEnumerable<Repository>> GetMyRepositoriesAsync(string query, int limit = 0)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            string url = _baseApiAddress + $"user/me/repos?q={query}";

            if (limit > 0)
                url += $"&limit={limit}";

            return await HttpClient.GetAsync<IEnumerable<Repository>>(url);
        }

        public async Task<IEnumerable<Repository>> GetMyRepositoriesAsync(bool unused = false)
        {
            string url = _baseApiAddress + "user/me/repos";

            if (unused)
                url += $"?type=unused";

            return await HttpClient.GetAsync<IEnumerable<Repository>>(url);
        }

        public async Task<IEnumerable<Repository>> GetRepositoriesAsync(string userId)
        {
            string url = _baseApiAddress + $"user/{userId}/repos";
            return await HttpClient.GetAsync<IEnumerable<Repository>>(url);
        }

        public async Task<IEnumerable<Room>> GetSuggestedRoomsAsync()
        {
            string url = _baseApiAddress + "user/me/suggestedRooms";
            return await HttpClient.GetAsync<IEnumerable<Room>>(url);
        }

        public async Task<IEnumerable<RoomUnreadCount>> GetAggregatedUnreadItemsAsync()
        {
            string url = _baseApiAddress + "user/me/unreadItems";
            return await HttpClient.GetAsync<IEnumerable<RoomUnreadCount>>(url);
        }

        public async Task<UserInfo> GetUserInfoAsync(string username)
        {
            string url = _baseApiAddress + $"users/{username}";
            return await HttpClient.GetAsync<UserInfo>(url);
        }

        #endregion

        #region Unread Items

        public async Task<UnreadItems> RetrieveUnreadChatMessagesAsync(string userId, string roomId)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/unreadItems";
            return await HttpClient.GetAsync<UnreadItems>(url);
        }

        public async Task MarkUnreadChatMessagesAsync(string userId, string roomId, IEnumerable<string> messageIds)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/unreadItems";

            var content = new StringContent("{\"chat\": " + JsonConvert.SerializeObject(messageIds) + "}",
                Encoding.UTF8,
                "application/json");

            await HttpClient.PostAsync(url, content);
        }

        #endregion

        #region Rooms

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            string url = _baseApiAddress + "rooms";
            return await HttpClient.GetAsync<IEnumerable<Room>>(url);
        }

        public async Task<IEnumerable<GitterUser>> GetRoomUsersAsync(string roomId, int limit = 30, string q = null, int skip = 0)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/users?limit={limit}";

            if (!string.IsNullOrWhiteSpace(q))
                url += $"&q={q}";

            if (skip > 0)
                url += $"&skip={skip}";

            return await HttpClient.GetAsync<IEnumerable<GitterUser>>(url);
        }

        public async Task<Room> JoinRoomAsync(string roomName)
        {
            string url = _baseApiAddress + "rooms";

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"uri", roomName}
            });

            return await HttpClient.PostAsync<Room>(url, content);
        }

        public async Task<Room> JoinRoomAsync(string userId, string roomId)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms";

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"id", roomId}
            });

            return await HttpClient.PostAsync<Room>(url, content);
        }

        public async Task<Room> UpdateRoomAsync(string roomId, UpdateRoomRequest request)
        {
            string url = _baseApiAddress + $"rooms/{roomId}";

            var content = new StringContent(JsonConvert.SerializeObject(request));

            return await HttpClient.PutAsync<Room>(url, content);
        }

        public async Task<bool> UpdateUserRoomSettingsAsync(string userId, string roomId, UpdateUserRoomSettingsRequest request)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}";

            var content = new StringContent(JsonConvert.SerializeObject(request));

            var response = await HttpClient.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<RoomNotificationSettingsResponse> GetRoomNotificationSettingsAsync(string userId, string roomId)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/settings/notifications";
            return await HttpClient.GetAsync<RoomNotificationSettingsResponse>(url);
        }

        public async Task<RoomNotificationSettingsResponse> UpdateRoomNotificationSettingsAsync(string userId, string roomId, UpdateRoomNotificationSettingsRequest request)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/settings/notifications";

            var content = new StringContent(JsonConvert.SerializeObject(request));

            return await HttpClient.PutAsync<RoomNotificationSettingsResponse>(url, content);
        }

        public async Task<SuccessResponse> LeaveRoomAsync(string roomId, string userId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/users/{userId}";
            return await HttpClient.DeleteAsync<SuccessResponse>(url);
        }

        public async Task<SuccessResponse> DeleteRoomAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}";
            return await HttpClient.DeleteAsync<SuccessResponse>(url);
        }

        public async Task<IEnumerable<Room>> GetSuggestedRoomsAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/suggestedRooms";
            return await HttpClient.GetAsync<IEnumerable<Room>>(url);
        }

        public async Task<IEnumerable<Collaborator>> GetSuggestedCollaboratorsOnRoomAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/collaborators";
            return await HttpClient.GetAsync<IEnumerable<Collaborator>>(url);
        }

        public async Task<IEnumerable<RoomIssue>> GetRoomIssuesAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/issues";
            return await HttpClient.GetAsync<IEnumerable<RoomIssue>>(url);
        }

        public async Task<IEnumerable<Ban>> GetRoomBansAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/bans";
            return await HttpClient.GetAsync<IEnumerable<Ban>>(url);
        }

        public async Task<BanUserResponse> BanUserFromRoomAsync(string roomId, string username, bool removeMessages = false)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/bans";

            var content = new StringContent(JsonConvert.SerializeObject(new BanUserFromRoomRequest
            {
                Username = username,
                RemoveMessages = removeMessages
            }));

            return await HttpClient.PostAsync<BanUserResponse>(url, content);
        }

        public async Task<SuccessResponse> UnbanUserAsync(string roomId, string userId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/bans/{userId}";
            return await HttpClient.DeleteAsync<SuccessResponse>(url);
        }

        public async Task<WelcomeMessage> GetWelcomeMessageAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/meta/welcome-message";
            return await HttpClient.GetAsync<WelcomeMessage>(url);
        }

        public async Task<UpdateWelcomeMessageResponse> UpdateWelcomeMessageAsync(string roomId, UpdateWelcomeMessageRequest request)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/meta/welcome-message";

            var content = new StringContent(JsonConvert.SerializeObject(request));

            return await HttpClient.PutAsync<UpdateWelcomeMessageResponse>(url, content);
        }

        public async Task<InviteUserResponse> InviteUserInRoomAsync(string roomId, InviteUserRequest request)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/invites";

            var content = new StringContent(JsonConvert.SerializeObject(request));

            return await HttpClient.PostAsync<InviteUserResponse>(url, content);
        }

        #endregion

        #region Messages

        public async Task<Message> GetSingleRoomMessageAsync(string roomId, string messageId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}";
            return await HttpClient.GetAsync<Message>(url);
        }

        public async Task<IEnumerable<Message>> GetRoomMessagesAsync(string roomId, MessageRequest request)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages?";

            url += $"limit={request.Limit}";

            if (!string.IsNullOrWhiteSpace(request.BeforeId))
                url += $"&beforeId={request.BeforeId}";

            if (!string.IsNullOrWhiteSpace(request.AfterId))
                url += $"&afterId={request.AfterId}";

            if (!string.IsNullOrWhiteSpace(request.AroundId))
                url += $"&aroundId={request.AroundId}";

            if (request.Skip > 0)
                url += $"&skip={request.Skip}";

            if (!string.IsNullOrWhiteSpace(request.Lang))
                url += $"&lang={request.Lang}";

            if (!string.IsNullOrWhiteSpace(request.Query))
                url += $"&q={request.Query}";

            return await HttpClient.GetAsync<IEnumerable<Message>>(url);
        }

        public async Task<Message> SendMessageAsync(string roomId, string message)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages";

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });

            return await HttpClient.PostAsync<Message>(url, content);
        }

        public async Task<Message> UpdateMessageAsync(string roomId, string messageId, string message)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}";

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"text", message}
            });

            return await HttpClient.PutAsync<Message>(url, content);
        }

        public async Task DeleteMessageAsync(string roomId, string messageId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}";
            await HttpClient.DeleteAsync(url);
        }

        public async Task<IEnumerable<GitterUser>> GetUsersWhoReadMessageAsync(string roomId, string messageId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/chatMessages/{messageId}/readBy";
            return await HttpClient.GetAsync<IEnumerable<GitterUser>>(url);
        }

        #endregion

        #region Events

        public async Task<IEnumerable<RoomEvent>> GetRoomEventsAsync(string roomId, int limit = 50, int skip = 0, string beforeId = null)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/events?limit={limit}";
                        
            if (skip > 0)
                url += $"&skip={skip}";

            if (!string.IsNullOrWhiteSpace(beforeId))
                url += $"&beforeId={beforeId}";

            return await HttpClient.GetAsync<IEnumerable<RoomEvent>>(url);
        }

        #endregion

        #region Groups

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            string url = _baseApiAddress + "groups";
            return await HttpClient.GetAsync<IEnumerable<Group>>(url);
        }

        public async Task<IEnumerable<Room>> GetGroupRoomsAsync(string groupId)
        {
            string url = _baseApiAddress + $"groups/{groupId}/rooms";
            return await HttpClient.GetAsync<IEnumerable<Room>>(url);
        }

        public async Task<Room> CreateRoomAsync(string groupId, CreateRoomRequest request)
        {
            string url = _baseApiAddress + $"groups/{groupId}/rooms";

            var content = new StringContent(JsonConvert.SerializeObject(request));

            return await HttpClient.PostAsync<Room>(url, content);
        }

        public async Task<IEnumerable<Room>> GetSuggestedRoomsFromGroupAsync(string groupId)
        {
            string url = _baseApiAddress + $"groups/{groupId}/suggestedRooms";
            return await HttpClient.GetAsync<IEnumerable<Room>>(url);
        }

        #endregion

        #region Search

        public async Task<SearchResponse<Room>> SearchRoomsAsync(string query, int limit = 10, int skip = 0)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            string url = _baseApiAddress + $"rooms?q={query}&limit={limit}&skip={skip}";
            return await HttpClient.GetAsync<SearchResponse<Room>>(url);
        }

        public async Task<SearchResponse<GitterUser>> SearchUsersAsync(string query, int limit = 10, int skip = 0)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            string url = _baseApiAddress + $"user?q={query}&limit={limit}&skip={skip}";
            return await HttpClient.GetAsync<SearchResponse<GitterUser>>(url);
        }

        public async Task<SearchResponse<Repository>> SearchUserRepositoriesAsync(string userId, string query, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            string url = _baseApiAddress + $"user/{userId}/repos?q={query}&limit={limit}";
            return await HttpClient.GetAsync<SearchResponse<Repository>>(url);
        }

        #endregion

        #region Analytics

        public async Task<Dictionary<DateTime, int>> GetRoomMessagesCountByDayAsync(string roomId)
        {
            string url = $"{Constants.ApiBaseUrl}/private/chat-heatmap/{roomId}";
            return await HttpClient.GetAsync<Dictionary<string, int>>(url)
                .ContinueWith(task =>
                {
                    var datesWithCount = new Dictionary<DateTime, int>();

                    foreach (var timestampWithCount in task.Result)
                    {
                        double timestamp = Convert.ToDouble(timestampWithCount.Key);
                        var dateNewKey = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);

                        datesWithCount.Add(dateNewKey, timestampWithCount.Value);
                    }

                    return datesWithCount;
                });
        }

        #endregion
    }
}