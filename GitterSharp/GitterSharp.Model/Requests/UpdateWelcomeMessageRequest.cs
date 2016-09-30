using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterSharp.Model.Requests
{
    public class UpdateWelcomeMessageRequest
    {
        [JsonProperty("welcomeMessage")]
        public string Content { get; set; }
    }
}
