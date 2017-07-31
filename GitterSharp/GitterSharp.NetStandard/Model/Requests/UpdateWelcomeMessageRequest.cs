using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class UpdateWelcomeMessageRequest
    {
        [JsonProperty("welcomeMessage")]
        public string Content { get; set; }
    }
}
