namespace Sparkle.Neo4j.Domain.Data
{
    public class GraphSettings : IGraphSettings
    {
        public string Url { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
