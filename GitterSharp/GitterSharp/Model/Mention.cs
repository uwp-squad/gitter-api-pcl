using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class Mention
    {
        [JsonProperty("screenName")]
        public string ScreenName { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
