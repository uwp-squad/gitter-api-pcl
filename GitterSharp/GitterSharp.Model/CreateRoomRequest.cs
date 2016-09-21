using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterSharp.Model
{
    public class CreateRoomRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        [JsonProperty("addBadge")]
        public bool AddBadge { get; set; }
    }
}
