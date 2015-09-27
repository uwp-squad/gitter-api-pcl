using Newtonsoft.Json;

namespace Gitter.Model
{
    public class Issue
    {
        [JsonProperty("number")]
        public string Number { get; set; }
    }
}
