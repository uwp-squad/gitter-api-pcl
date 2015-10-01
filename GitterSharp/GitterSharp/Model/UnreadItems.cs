using System.Collections.Generic;
using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class UnreadItems
    {
        [JsonProperty("chat")]
        IEnumerable<string> UnreadMessages { get; set; }

        [JsonProperty("mention")]
        IEnumerable<string> UnreadMentions { get; set; }
    }
}