using MongoDB.Driver;
using Sparkle.Domain.Data;
using Sparkle.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        private string pattern = "{title:1,body:1}";

        /// <summary>
        /// Add new post into database
        /// </summary>
        /// <param name="post"></param>
        public void Create(Post post)
        {
            _posts.InsertOne(post);
        }

        /// <summary>
        /// Get all posts from DataBase
        /// </summary>
        /// <returns></returns>
        public List<Post> Get()
        {
            return _posts.Find("{}").Project<Post>(pattern).ToList();
        }


        /// <summary>
        /// Get all posts from DataBase async
        /// </summary>
        /// <returns></returns>
        public Task<List<Post>> GetAsync()
        {
            return _posts.Find("{}").Project<Post>(pattern).ToListAsync();
        }


        #endregion
    }
}
