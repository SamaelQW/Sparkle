using MongoDB.Driver;
using Sparkle.Domain.Data;
using Sparkle.Domain.Entities;
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

        public void Create(User user)
        {
            _users.InsertOne(user);
        }

        public void Delete(User user)
        {
            _users.DeleteOne(u => u.Id == user.Id);
        }

        public User Get(string id)
        {
            return _users.Find($"_id: {id}").FirstOrDefault();

        }

        public User GetByUserName(string userName)
        {
            var filterBuilder = new FilterDefinitionBuilder<User>();
            var filter = filterBuilder.Eq("username", userName);
            return _users.Find(filter).FirstOrDefault();

        }

        public void Update(User user)
        {
            _users.ReplaceOne(u => u.Id == user.Id, user);
        }
        #endregion
        #region Async
        public Task CreateAsync(User user)
        {
            return _users.InsertOneAsync(user);
        }

        public Task DeleteAsync(User user)
        {
            return _users.DeleteOneAsync(u => u.Id == user.Id);
        }

        public Task<User> GetAsync(string id)
        {
            return _users.FindAsync($"_id: {id}").Result.FirstOrDefaultAsync();
        }

        public Task<User> GetByUserNameAsync(string userName)
        {
            var filterBuilder = new FilterDefinitionBuilder<User>();
            var filter = filterBuilder.Eq("username", userName);
            return _users.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public Task UpdateAsync(User user)
        {
            return _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
        #endregion
        #endregion

    }
}
