using Newtonsoft.Json;

namespace GitterSharp.Model.Responses
{
    public class RoomNotificationSettingsResponse
    {
        [JsonProperty("push")]
        public string Push { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("lurk")]
        public bool Lurk { get; set; }

        [JsonProperty("unread")]
        public bool Unread { get; set; }

        [JsonProperty("activity")]
        public bool Activity { get; set; }

        [JsonProperty("mention")]
        public bool Mention { get; set; }

        [JsonProperty("announcement")]
        public bool Announcement { get; set; }

        [JsonProperty("desktop")]
        public bool Desktop { get; set; }

        [JsonProperty("mobile")]
        public bool Mobile { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("defaultSettings")]
        public DefaultNotificationSettingsReponse DefaultSettings { get; set; }
    }

    public class DefaultNotificationSettingsReponse
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("lurk")]
        public bool Lurk { get; set; }

        [JsonProperty("flags")]
        public int Flags { get; set; }

        [JsonProperty("unread")]
        public bool Unread { get; set; }

        [JsonProperty("activity")]
        public bool Activity { get; set; }

        [JsonProperty("mention")]
        public bool Mention { get; set; }

        [JsonProperty("announcement")]
        public bool Announcement { get; set; }

        [JsonProperty("desktop")]
        public bool Desktop { get; set; }

        [JsonProperty("mobile")]
        public bool Mobile { get; set; }
    }
}
