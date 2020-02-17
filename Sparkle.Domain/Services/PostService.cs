using MongoDB.Driver;
using Sparkle.Domain.Data;
using Sparkle.Domain.Entities;
namespace Sparkle.Domain.Services
{
    public class PostService
    {

        #region Private Members
        private readonly IMongoCollection<Post> _posts;

        #endregion


        #region Constructor
        /// <summary>
        /// Constructor which can do things with <see cref="Post">Posts</see> collection
        /// </summary>
        /// <param name="settings">DataBase settings <see cref="ISparkleDatabaseSettings"/>ISparkleDatabaseSettings</param>
        public PostService(ISparkleDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _posts = database.GetCollection<Post>(settings.PostsCollectionName);
        }
        #endregion


        #region Public Members




        #endregion
    }
}
