using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba
{
    class Item
    {
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("attributes")]
        public Dictionary<string, object> attributes { get; set; }
    }
}
