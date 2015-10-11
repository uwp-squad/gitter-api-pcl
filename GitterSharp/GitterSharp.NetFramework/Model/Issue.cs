using Newtonsoft.Json;

namespace GitterSharp.Model
{
    public class Issue
    {
        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
