using Newtonsoft.Json;

namespace Gitter.Model
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("avatarUrlSmall")]
        public string SmallAvatarUrl { get; set; }

        [JsonProperty("avatarUrlMedium")]
        public string MediumAvatarUrl { get; set; }
    }
}
