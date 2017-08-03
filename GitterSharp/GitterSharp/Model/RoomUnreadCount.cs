using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class RoomUnreadCount
    {
        [JsonProperty("id")]
        public string RoomId { get; set; }

        [JsonProperty("uri")]
        public string RoomUri { get; set; }

        [JsonProperty("unreadItems")]
        public string UnreadItemsCount { get; set; }

        [JsonProperty("mentions")]
        public string MentionsCount { get; set; }
    }
}
