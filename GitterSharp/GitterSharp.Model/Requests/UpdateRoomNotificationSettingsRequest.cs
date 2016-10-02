using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
