using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
