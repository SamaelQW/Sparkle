using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sparkle.Domain.Data;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using System.Collections.Generic;
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


            UserService service = new UserService(GetSettings());

            //service.Create(user);
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

            var user = service.GetByUserName("test");
            // service.Delete(user);
            Assert.AreEqual(Init(), user);
        }

        [TestMethod]
        public void UserService_GetFriends()
        {
            var userService = new UserService(GetSettings());
            var user = Init();


            userService.Create(user);
            var result = (List<Friend>)user.Friends;
            List<Friend> friends;
            try
            {
                friends = (List<Friend>)userService.GetFriends(user.UserName);

            }
            finally
            {
                userService.Delete(user);

            }

            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i], friends[i]);
            }

        }

        [TestMethod]
        public void FriendService_AddFriend()
        {
            UserService userService = new UserService(GetSettings());
            FriendService friendService = new FriendService(userService);
            var friend = new Friend()
            {
                FriendId = userService.GetByUserName("sofa").Id,
                Name = userService.GetByUserName("sofa").Name,
                Surname = userService.GetByUserName("sofa").Surname,
                Username = "sofa",
            };
            friendService.Add(userService.GetByUserName("admin"), friend);

            Assert.AreEqual(friend, ((List<Friend>)userService.GetFriends("admin"))[0]);


        }

        private SparkleDatabaseSettings GetSettings()
        {
            return new SparkleDatabaseSettings()
            {
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "network",
                UsersCollectionName = "users",
                PostsCollectionName = "posts",
            };
        }

        private User Init()
        {


            User user = new User()
            {
                Name = "test",
                Age = 18,
                Email = "test",
                Password = "test",
                Status = EUserStatus.Active,
                Surname = "test",
                UserName = "test",
                Friends = new List<Friend> { new Friend { Name = "Taras" }, new Friend { Name = "Cat" } },
            };
            return user;
        }
    }
}
