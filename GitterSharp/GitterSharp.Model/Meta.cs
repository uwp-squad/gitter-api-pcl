using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterSharp.Model
{
    public class Meta
    {
        [JsonProperty("repo")]
        public string Repo { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
