using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class RoomIssue
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
