using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class UpdateUserRoomSettingsRequest
    {
        [JsonProperty("favourite")]
        public bool Favourite { get; set; }
    }
}
