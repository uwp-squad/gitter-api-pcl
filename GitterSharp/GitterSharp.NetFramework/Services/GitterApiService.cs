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

        public async Task<IEnumerable<Room>> GetSuggestedRoomsAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/suggestedRooms";
            return await HttpClient.GetAsync<IEnumerable<Room>>(url);
        }

        public async Task<WelcomeMessage> GetWelcomeMessageAsync(string roomId)
        {
            string url = _baseApiAddress + $"rooms/{roomId}/meta/welcome-message";
            return await HttpClient.GetAsync<WelcomeMessage>(url);
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
    }
}