using Newtonsoft.Json;

namespace GitterSharp.Model.Realtime
{
    public class RealtimeChatMessage
    {
        /// <summary>
        /// "create" or "update" or "remove"
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("model")]
        public Message Model { get; set; }
    }
}
