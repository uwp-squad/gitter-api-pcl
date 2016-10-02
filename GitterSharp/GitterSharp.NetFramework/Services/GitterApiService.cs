using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using GitterSharp.Configuration;
using GitterSharp.Helpers;
using GitterSharp.Model;
using Newtonsoft.Json;
using GitterSharp.Model.Requests;
using GitterSharp.Model.Responses;
#if __IOS__ || __ANDROID__ || NET45
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
#endif
#if NETFX_CORE
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding;
#endif

namespace GitterSharp.Services
{
    public class GitterApiService : IGitterApiService
    {
        #region Fields

        private readonly string _baseApiAddress = $"{Constants.ApiBaseUrl}{Constants.ApiVersion}";

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

        public GitterApiService() { }

        public GitterApiService(string token)
        {
            Token = token;
        }

        #endregion

        #region User

        public async Task<User> GetCurrentUserAsync()
        {
            string url = _baseApiAddress + "user/me";
            return await HttpClient.GetAsync<User>(url);
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync(string userId)
        {
            string url = _baseApiAddress + $"user/{userId}/orgs";
            return await HttpClient.GetAsync<IEnumerable<Organization>>(url);
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

            await HttpClient.PostAsync(url, content);
        }

        #endregion

        #region Rooms

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            string url = _baseApiAddress + "rooms";
            return await HttpClient.GetAsync<IEnumerable<Room>>(url);
        }

        public async Task<IEnumerable<User>> GetRoomUsersAsync(string roomId, int limit = 30, string q = null, int skip = 0)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/users?limit={limit}";

            if (!string.IsNullOrWhiteSpace(q))
                url += $"&q={q}";

            if (skip > 0)
                url += $"&skip={skip}";

            return await HttpClient.GetAsync<IEnumerable<User>>(url);
        }

        public async Task<Room> JoinRoomAsync(string roomName)
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

            return await HttpClient.PostAsync<Room>(url, content);
        }

        public async Task<Room> JoinRoomAsync(string userId, string roomId)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms";

#if __IOS__ || __ANDROID__ || NET45
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"id", roomId}
            });
#endif
#if NETFX_CORE
            var content = new HttpFormUrlEncodedContent(new Dictionary<string, string>
            {
                {"id", roomId}
            });
#endif

            return await HttpClient.PostAsync<Room>(url, content);
        }

        public async Task<Room> UpdateRoomAsync(string roomId, UpdateRoomRequest request)
        {
            string url = _baseApiAddress + $"rooms/{roomId}";

#if __IOS__ || __ANDROID__ || NET45
            var content = new StringContent(JsonConvert.SerializeObject(request));
#endif
#if NETFX_CORE
            var content = new HttpStringContent(JsonConvert.SerializeObject(request));
#endif

            return await HttpClient.PutAsync<Room>(url, content);
        }

        public async Task<bool> UpdateUserRoomSettingsAsync(string userId, string roomId, UpdateUserRoomSettingsRequest request)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}";

#if __IOS__ || __ANDROID__ || NET45
            var content = new StringContent(JsonConvert.SerializeObject(request));
#endif
#if NETFX_CORE
            var content = new HttpStringContent(JsonConvert.SerializeObject(request));
#endif

            var response = await HttpClient.PutAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<RoomNotificationSettingsResponse> UpdateRoomNotificationSettingsAsync(string userId, string roomId, UpdateRoomNotificationSettingsRequest request)
        {
            string url = _baseApiAddress + $"user/{userId}/rooms/{roomId}/settings/notifications";

#if __IOS__ || __ANDROID__ || NET45
            var content = new StringContent(JsonConvert.SerializeObject(request));
#endif
#if NETFX_CORE
            var content = new HttpStringContent(JsonConvert.SerializeObject(request));
#endif

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

        public async Task<WelcomeMessage> GetWelcomeMessageAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/meta/welcome-message";
            return await HttpClient.GetAsync<WelcomeMessage>(url);
        }

        public async Task<UpdateWelcomeMessageResponse> UpdateWelcomeMessageAsync(string roomId, UpdateWelcomeMessageRequest request)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/meta/welcome-message";

#if __IOS__ || __ANDROID__ || NET45
            var content = new StringContent(JsonConvert.SerializeObject(request));
#endif
#if NETFX_CORE
            var content = new HttpStringContent(JsonConvert.SerializeObject(request));
#endif

            return await HttpClient.PutAsync<UpdateWelcomeMessageResponse>(url, content);
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

            return await HttpClient.PostAsync<Message>(url, content);
        }

        public async Task<Message> UpdateMessageAsync(string roomId, string messageId, string message)
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

            return await HttpClient.PutAsync<Message>(url, content);
        }

        #endregion

        #region Events

        public async Task<IEnumerable<RoomEvent>> GetRoomEventsAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/events";
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
            string url = _baseApiAddress + $"groups/${groupId}/rooms";

#if __IOS__ || __ANDROID__ || NET45
            var content = new StringContent(JsonConvert.SerializeObject(request));
#endif
#if NETFX_CORE
            var content = new HttpStringContent(JsonConvert.SerializeObject(request));
#endif

            return await HttpClient.PostAsync<Room>(url, content);
        }

        #endregion

        #region Search

        public async Task<SearchResponse<Room>> SearchRoomsAsync(string query, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            string url = _baseApiAddress + $"rooms?q={query}&limit={limit}";
            return await HttpClient.GetAsync<SearchResponse<Room>>(url);
        }

        public async Task<SearchResponse<User>> SearchUsersAsync(string query, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            string url = _baseApiAddress + $"user?q={query}&limit={limit}";
            return await HttpClient.GetAsync<SearchResponse<User>>(url);
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