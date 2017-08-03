using Newtonsoft.Json;

namespace GitterSharp.Model.Responses
{
    public class SuccessResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
