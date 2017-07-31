using Newtonsoft.Json;
using System.Collections.Generic;

namespace GitterSharp.Model.Responses
{
    public class SearchResponse<T>
    {
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
    }
}
