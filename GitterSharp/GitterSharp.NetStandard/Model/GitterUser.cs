using Newtonsoft.Json;
using System.Collections.Generic;

namespace GitterSharp.Model
{
    public class GitterUser
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

        [JsonProperty("providers")]
        public IEnumerable<string> Providers { get; set; }

        [JsonProperty("staff")]
        public bool Staff { get; set; }

        [JsonProperty("v")]
        public int Version { get; set; }

        [JsonProperty("gv")]
        public string GravatarVersion { get; set; }

        [JsonIgnore]
        public string GitHubUrl { get { return $"https://github.com{Url}"; } }
    }
}
