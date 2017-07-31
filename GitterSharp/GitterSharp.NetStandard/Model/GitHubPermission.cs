using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class GitHubPermission
    {
        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("push")]
        public bool Push { get; set; }

        [JsonProperty("pull")]
        public bool Pull { get; set; }
    }
}
