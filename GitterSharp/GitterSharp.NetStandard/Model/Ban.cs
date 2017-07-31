using Newtonsoft.Json;
using System;

namespace GitterSharp.Model
{
    public class Ban
    {
        [JsonProperty("user")]
        public GitterUser User { get; set; }

        [JsonProperty("bannedBy")]
        public GitterUser BannedBy { get; set; }

        [JsonProperty("dateBanned")]
        public DateTime Date { get; set; }
    }
}
