using MongoDB.Driver;
using Sparkle.Domain.Data;
using Sparkle.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparkle.Domain.Services
{
    public class UserService
    {
        #region Private members
        /// <summary>
        /// Collection contains users in database
        /// </summary>
        private readonly IMongoCollection<User> _users;
        private const string pattern = "{usersPosts:0}";

        #endregion
        /// <summary>
        /// Set up users collection from database using <paramref name="settings"/>
        /// </summary>
        /// <param name="settings">Database settings</param>
        public UserService(ISparkleDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UsersCollectionName);

        }

        #region Public Methods

        #region Sync
        /// <summary>
        /// Add an <paramref name="user"/> in database
        /// </summary>
        /// <param name="user">User entity to create</param>
        public void Create(User user)
        {
            _users.InsertOne(user);
        }

        /// <summary>
        /// Delete <paramref name="user"/> from database by id
        /// </summary>
        /// <param name="user">User to delete must contain id</param>
        public void Delete(User user)
        {
            _users.DeleteOne(u => u.Id == user.Id);
        }

        /// <summary>
        /// Get all users from db
        /// </summary>
        /// <returns></returns>
        public List<User> Get()
        {
            return _users.Find("{}").ToList();
        }


        /// <summary>
        /// Get <see cref="User"/> from database by id
        /// </summary>
        /// <param name="id">User's id</param>
        /// <returns>User with <paramref name="id"/></returns>
        public User Get(string id)
        {
            return _users.Find($"_id: {id}").FirstOrDefault();

        }

        /// <summary>
        /// Get <see cref="User"/> from database by usernama
        /// </summary>
        /// <param name="userName">User's username</param>
        /// <returns>User with <paramref name="userName"/></returns>
        public User GetByUserName(string userName)
        {
            var filterBuilder = new FilterDefinitionBuilder<User>();
            var filter = filterBuilder.Eq("username", userName);
            return _users.Find(filter).FirstOrDefault();

        }

        /// <summary>
        /// Update <paramref name="user"/> in database by id
        /// </summary>
        /// <param name="user">User to update</param>
        public void Update(User user)
        {
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }
        #endregion
        #region Async

        /// <summary>
        /// Add user in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task CreateAsync(User user)
        {
            return _users.InsertOneAsync(user);
        }

        /// <summary>
        /// Remove user from db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAsync(User user)
        {
            return _users.DeleteOneAsync(u => u.Id == user.Id);
        }

        /// <summary>
        /// Get all users from db
        /// </summary>
        /// <returns></returns>
        public Task<List<User>> GetAsync()
        {
            return _users.FindAsync("{}").Result.ToListAsync();
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<User> GetAsync(string id)
        {
            return _users.FindAsync($"_id: {id}").Result.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<User> GetByUserNameAsync(string userName)
        {
            var filterBuilder = new FilterDefinitionBuilder<User>();
            var filter = filterBuilder.Eq("username", userName);
            return _users.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        /// <summary>
        ///  Update user in db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task UpdateAsync(User user)
        {
            return _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
        #endregion
        #endregion

    }
}
