using Newtonsoft.Json;

namespace GitterSharp.Model.Responses
{
    public class InviteUserResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("email")]
        public string Mail { get; set; }

        [JsonProperty("user")]
        public GitterUser User { get; set; }

        [JsonProperty("avatarUrl")]
        public string AvatarUrl { get; set; }
    }
}
