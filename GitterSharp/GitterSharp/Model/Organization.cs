using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class Organization
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }

        [JsonProperty("room")]
        public Room Room { get; set; }
    }
}
