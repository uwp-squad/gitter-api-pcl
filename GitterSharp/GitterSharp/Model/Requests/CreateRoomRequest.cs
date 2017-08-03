using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class CreateRoomRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("topic")]
        public string Topic { get; set; }

        /// <summary>
        /// If null, will create a Public room
        /// </summary>
        [JsonProperty("security")]
        public CreateRoomSecurityRequest Security { get; set; }

        [JsonProperty("addBadge")]
        public bool AddBadge { get; set; }
    }

    public class CreateRoomSecurityRequest
    {
        /// <summary>
        /// Required for GitHub repository ('GH_REPO') or GitHub Org ('GH_ORG') (link to GitHub)
        /// Should be empty if anything else
        /// </summary>
        [JsonProperty("linkPath")]
        public string LinkPath { get; set; }

        /// <summary>
        /// Either 'PUBLIC' or 'PRIVATE'
        /// </summary>
        [JsonProperty("security")]
        public string SecurityType { get; set; }

        /// <summary>
        /// Room type : either null or 'GROUP' or 'GH_REPO' or 'GH_ORG'
        /// </summary>
        [JsonProperty("type")]
        public string RoomType { get; set; }
    }
}
