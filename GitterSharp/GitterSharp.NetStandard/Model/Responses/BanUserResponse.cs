using Newtonsoft.Json;

namespace GitterSharp.Model.Responses
{
    public class BanUserResponse
    {
        [JsonProperty("removed")]
        public bool Removed { get; set; }
    }
}
