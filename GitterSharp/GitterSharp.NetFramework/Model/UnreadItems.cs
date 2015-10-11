using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class UnreadItems
    {
        [JsonProperty("chat")]
        public IEnumerable<string> Messages { get; set; }

        [JsonProperty("mention")]
        public IEnumerable<string> Mentions { get; set; }
    }
}