using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba
{
    class Form
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("postmessage")]
        public string postmessage { get; set; }
        [JsonProperty("items")]
        public List<Item> items { get; set; }

    }
    
}

