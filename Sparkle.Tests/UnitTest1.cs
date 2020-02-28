using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sparkle.Domain.Data;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using Xunit.Sdk;

namespace Sparkle.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UserService_Create()
        {
            var user = Init();

            SparkleDatabaseSettings settings = new SparkleDatabaseSettings()
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "network",
                UsersCollectionName = "users",
                PostsCollectionName = "posts",
            };

            UserService service = new UserService(settings);

            service.Create(user);
            var result = service.GetByUserName("admin");
            user.Id = result.Id;
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void UserService_Delete()
        {
            SparkleDatabaseSettings settings = new SparkleDatabaseSettings()
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "network",
                UsersCollectionName = "users",
                PostsCollectionName = "posts",
            };

            UserService service = new UserService(settings);

            var user = service.GetByUserName("admin");
            service.Delete(user);

        }

        


        private User Init()
        {
            User user = new User()
            {
                Name = "Taras",
                Age = 18,
                Email = "mail",
                Password = "admin",
                Status = EUserStatus.Active,
                Surname = "Sharko",
                UserName = "admin"
            };
            return user;
        }
    }
}
