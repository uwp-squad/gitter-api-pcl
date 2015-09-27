using Newtonsoft.Json;

namespace Gitter.Model
{
    public class MessageUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
