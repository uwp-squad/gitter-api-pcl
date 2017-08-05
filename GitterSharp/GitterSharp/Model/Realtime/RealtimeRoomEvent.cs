using Newtonsoft.Json;

namespace GitterSharp.Model.Realtime
{
    public class RealtimeRoomEvent
    {
        /// <summary>
        /// "create"
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("model")]
        public RoomEvent Model { get; set; }
    }
}
