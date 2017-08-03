using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class BanUserFromRoomRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("removeMessages")]
        public bool RemoveMessages { get; set; }
    }
}
