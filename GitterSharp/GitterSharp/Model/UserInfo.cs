using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class UserInfo
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("email")]
        public string Mail { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("profile")]
        public string GitHubProfile { get; set; }

        [JsonProperty("gravatarImageUrl")]
        public string GravatarImageUrl { get; set; }

        [JsonProperty("github")]
        public GitHubInfo GitHubInfo { get; set; }
    }

    public class GitHubInfo
    {
        [JsonProperty("followers")]
        public int Followers { get; set; }

        [JsonProperty("public_repos")]
        public int PublicRepos { get; set; }

        [JsonProperty("following")]
        public int Following { get; set; }
    }
}
