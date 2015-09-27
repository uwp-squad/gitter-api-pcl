using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Gitter.Model
{
    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("sent")]
        public DateTime SentDate { get; set; }

        [JsonProperty("editedAt")]
        public DateTime? EditedDate { get; set; }

        [JsonProperty("fromUser")]
        public User User { get; set; }

        [JsonProperty("unread")]
        public bool UnreadByCurrent { get; set; }

        [JsonProperty("readBy")]
        public int ReadCount { get; set; }

        [JsonProperty("urls")]
        public IEnumerable<MessageUrl> Urls { get; set; }

        [JsonProperty("mentions")]
        public IEnumerable<Mention> Mentions { get; set; }

        [JsonProperty("issues")]
        public IEnumerable<Issue> Issues { get; set; }

        [JsonProperty("v")]
        public int Version { get; set; }
    }
}
