using Newtonsoft.Json;
using System;

namespace GitterSharp.Model
{
    public class RoomEvent
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

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        // TODO : What to do with payload ?

        [JsonProperty("v")]
        public int Version { get; set; }
    }
}
