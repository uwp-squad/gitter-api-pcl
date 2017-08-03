using Newtonsoft.Json;

namespace GitterSharp.Model.Requests
{
    public class UpdateRoomNotificationSettingsRequest
    {
        /// <summary>
        /// <see cref="NotificationMode"/>
        /// </summary>
        [JsonProperty("mode")]
        public string Mode { get; set; }
    }
}
