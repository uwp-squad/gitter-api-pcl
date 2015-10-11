using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class MessageUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
