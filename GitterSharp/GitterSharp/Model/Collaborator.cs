using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class Collaborator
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
