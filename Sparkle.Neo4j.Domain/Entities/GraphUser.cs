using Newtonsoft.Json;

namespace Sparkle.Neo4j.Domain.Entities
{
    public class GraphUser
    {
        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }
    }
}
