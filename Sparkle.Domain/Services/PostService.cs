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

        private string pattern = "{title:1,body:1,createdDate:1}";
        #region Async Methods

        /// <summary>
        /// Get all posts from DataBase
        /// </summary>
        /// <returns></returns>
        public Task<List<Post>> GetAsync()
        {
            return _posts.Find("{}").Project<Post>(pattern).ToListAsync();
        }
        /// <summary>
        /// Delete the <paramref name="post"/> from database
        /// </summary>
        /// <param name="post">Post to delete</param>
        public Task RemoveAsync(Post post)
        {
            return _posts.DeleteOneAsync(p => p.Id == post.Id);
        }

        /// <summary>
        /// Delete the <see cref="Post"/> from database by <paramref name="id"/>
        /// </summary>
        /// <param name="id">The post id</param>
        public Task RemoveAsync(string id)
        {
            return _posts.DeleteOneAsync(p => p.Id == id);
        }

        /// <summary>
        /// Update the post by <paramref name="postId"/> 
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public Task UpdateAsync(string postId, Post post)
        {
            return _posts.ReplaceOneAsync(p => p.Id == post.Id, post);
        }
        #endregion

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
        /// Update the post by <paramref name="postId"/>
        /// </summary>
        /// <param name="postId">The updated post id</param>
        /// <param name="post">Post with new data</param>
        public void Update(string postId, Post post)
        {
            _posts.ReplaceOne(p => p.Id == post.Id, post);
        }

        /// <summary>
        /// Delete the <paramref name="post"/> from database
        /// </summary>
        /// <param name="post">Post to delete</param>
        public void Remove(Post post)
        {
            _posts.DeleteOne(p => p.Id == post.Id);
        }

        /// <summary>
        /// Delete the <see cref="Post"/> from database by <paramref name="id"/>
        /// </summary>
        /// <param name="id">The post id</param>
        public void Remove(string id)
        {
            _posts.DeleteOne(p => p.Id == id);
        }



        #endregion
    }
}
