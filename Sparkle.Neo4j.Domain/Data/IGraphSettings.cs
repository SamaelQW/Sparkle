namespace Sparkle.Neo4j.Domain.Data
{
    public interface IGraphSettings
    {
        string Url { get; set; }

        string Login { get; set; }

        string Password { get; set; }
    }
}
