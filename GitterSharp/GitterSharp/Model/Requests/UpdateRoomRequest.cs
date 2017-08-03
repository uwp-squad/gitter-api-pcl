using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class UpdateRoomRequest
    {
        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("noindex")]
        public bool NoIndex { get; set; }

        /// <summary>
        /// Example: "gitter, api, csharp"
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; set; }
    }
}
