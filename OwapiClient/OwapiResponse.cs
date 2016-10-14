using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OwapiClient
{
    class OwapiResponse
    {
        [JsonProperty("_request")]
        public RequestData Request { get; set; }
        [JsonProperty("any")]
        public Region AnyRegion { get; set; }
        [JsonProperty("eu")]
        public Region EuRegion { get; set; }
        [JsonProperty("kr")]
        public Region KrRegion { get; set; }
        [JsonProperty("us")]
        public Region UsRegion { get; set; }
    }
}
