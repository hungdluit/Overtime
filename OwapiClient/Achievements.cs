using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace OwapiClient
{
    public class Achievements
    {
        [JsonProperty("defense")]
        public Defense Defense { get; set; }
        [JsonProperty("general")]
        public General General { get; set; }
        [JsonProperty("maps")]
        public Maps Maps { get; set; }
        [JsonProperty("offense")]
        public Offense Offense { get; set; }
        [JsonProperty("support")]
        public Support Support { get; set; }
        [JsonProperty("tank")]
        public Tank Tank { get; set; }
    }
}