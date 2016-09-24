using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterSharp.Model.Responses
{
    public class SearchResponse<T>
    {
        [JsonProperty("results")]
        public IEnumerable<T> Results { get; set; }
    }
}
