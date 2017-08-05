using Newtonsoft.Json;

namespace GitterSharp.Model.Realtime
{
    public class RealtimeReadBy
    {
        /// <summary>
        /// "create"
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("model")]
        public GitterUser Model { get; set; }
    }
}
