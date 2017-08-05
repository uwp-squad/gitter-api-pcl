using Newtonsoft.Json;

namespace GitterSharp.Model.Realtime
{
    public class RealtimeUserPresence
    {
        /// <summary>
        /// Should be "presence"
        /// </summary>
        [JsonProperty("notification")]
        public string Notification { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// "in": user entered
        /// "out": user left
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
