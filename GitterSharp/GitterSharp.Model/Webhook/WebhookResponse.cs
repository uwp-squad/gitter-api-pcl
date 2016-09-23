using Newtonsoft.Json;

namespace GitterSharp.Model.Webhook
{
    public class WebhookResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}