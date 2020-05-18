namespace Sparkle.Domain.Data
{
    public class SparkleDatabaseSettings : ISparkleDatabaseSettings
    {
        public string PostsCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
