using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class InviteUserRequest
    {
        /// <summary>
        /// "email": invite by mail
        /// "gitter": invite using gitter id
        /// "github": invite using GitHub account (mail and id)
        /// "twitter": invite using Twitter account (mail and id)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("externalId")]
        public long ExternalId { get; set; }

        [JsonProperty("emailAddress")]
        public string Mail { get; set; }
    }
}
