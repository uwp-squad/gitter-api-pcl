using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class UnreadItems
    {
        [JsonProperty("chat")]
        IEnumerable<string> Messages { get; set; }

        [JsonProperty("mention")]
        IEnumerable<string> Mentions { get; set; }
    }
}