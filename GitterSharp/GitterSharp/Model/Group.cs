using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("backedBy")]
        public GroupSecurity BackedBy { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }
    }
}
