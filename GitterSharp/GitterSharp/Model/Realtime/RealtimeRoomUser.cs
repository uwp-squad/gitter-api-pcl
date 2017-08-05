using Newtonsoft.Json;

namespace GitterSharp.Model.Realtime
{
    public class RealtimeRoomUser
    {
        /// <summary>
        /// "create" or "remove"
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("model")]
        public GitterUser Model { get; set; }
    }
}
