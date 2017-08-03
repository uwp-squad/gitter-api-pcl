using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class WelcomeMessage
    {
        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
