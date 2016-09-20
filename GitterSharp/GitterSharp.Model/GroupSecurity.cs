using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitterSharp.Model
{
    public class GroupSecurity
    {
        /// <summary>
        /// Type of the group
        /// Refers to <see cref="GroupType"/>
        /// </summary>
        [JsonProperty("type")]        
        public string Type { get; set; }

        [JsonProperty("linkPath")]
        public string LinkPath { get; set; }
    }
}
