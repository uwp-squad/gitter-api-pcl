using Newtonsoft.Json;
using System.Collections.Generic;

namespace GitterSharp.Model.Responses
{
    public class UpdateWelcomeMessageResponse
    {
        [JsonProperty("welcomeMessage")]
        public UpdateWelcomeMessageSubResponse Result { get; set; }
    }

    public class UpdateWelcomeMessageSubResponse
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("urls")]
        public List<MessageUrl> Urls { get; set; }

        [JsonProperty("urls")]
        public List<Mention> Mentions { get; set; }

        [JsonProperty("issues")]
        public List<Issue> Issues { get; set; }

        [JsonProperty("plainText")]
        public string PlainText { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}
