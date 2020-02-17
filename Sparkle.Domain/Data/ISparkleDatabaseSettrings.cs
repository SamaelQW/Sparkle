namespace Sparkle.Domain.Data
{
    public interface ISparkleDatabaseSettings
    {
        string PostsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
