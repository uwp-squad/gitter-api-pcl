using Newtonsoft.Json;

namespace Gitter.Model
{
    public class Mention
    {
        [JsonProperty("screenName")]
        public string ScreenName { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
