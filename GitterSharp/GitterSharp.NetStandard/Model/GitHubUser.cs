using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class GitHubUser
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("login")]
        public string Username { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("gravatar_id")]
        public string GravatarId { get; set; }

        [JsonProperty("url")]
        public string ApiUrl { get; set; }

        [JsonProperty("html_url")]
        public string WebsiteUrl { get; set; }

        [JsonProperty("followers_url")]
        public string FollowersApiUrl { get; set; }

        [JsonProperty("following_url")]
        public string FollowingApiUrl { get; set; }

        [JsonProperty("gists_url")]
        public string GistsApiUrl { get; set; }

        [JsonProperty("starred_url")]
        public string StarredApiUrl { get; set; }

        [JsonProperty("subscriptions_url")]
        public string SubscriptionsApiUrl { get; set; }

        [JsonProperty("organizations_url")]
        public string OrgsApiUrl { get; set; }

        [JsonProperty("repos_url")]
        public string ReposApiUrl { get; set; }

        [JsonProperty("events_url")]
        public string EventsApiUrl { get; set; }

        [JsonProperty("received_events_url")]
        public string ReceivedEventsApiUrl { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("site_admin")]
        public bool IsGitHubAdmin { get; set; }
    }
}
