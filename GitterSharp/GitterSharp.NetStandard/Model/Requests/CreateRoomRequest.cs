using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class CreateRoomRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("addBadge")]
        public bool AddBadge { get; set; }
    }
}
