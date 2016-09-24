using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterSharp.Model.Requests
{
    public class UpdateUserRoomSettingsRequest
    {
        [JsonProperty("favourite")]
        public bool Favourite { get; set; }
    }
}
