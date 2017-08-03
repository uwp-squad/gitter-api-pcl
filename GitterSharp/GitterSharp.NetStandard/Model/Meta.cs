using Newtonsoft.Json;
using System.Collections.Generic;

namespace GitterSharp.Model
{
    public class Meta
    {
        [JsonProperty("repo")]
        public string Repo { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("branch")]
        public string Branch { get; set; }

        [JsonProperty("commits")]
        public IEnumerable<CommitMeta> Commits { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class CommitMeta
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }
    }
}
