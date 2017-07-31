using Newtonsoft.Json;
using System;

namespace GitterSharp.Model
{
    public class Ban
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("bannedBy")]
        public User BannedBy { get; set; }

        [JsonProperty("dateBanned")]
        public DateTime Date { get; set; }
    }
}
