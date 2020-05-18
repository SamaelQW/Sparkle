using Neo4jClient;
using Sparkle.Domain.Core;
using Sparkle.Domain.Entities;
using Sparkle.Neo4j.Domain.Entities;
using System.Linq;

namespace Sparkle.Neo4j.Domain.Services
{
    public class GraphUserService
    {
        private readonly GraphClient _client;

        public GraphUserService(Factory.GraphClientFactory factory)
        {
            _client = factory.GetClient();
            _client.Connect();
        }

        #region Create
        public void Create(User user)
        {
            GraphUser u = new GraphUser()
            {
                Login = user.UserName
            };

            _client.Cypher
                .Create("(u:User {user})")
                .WithParam("user", u)
                .ExecuteWithoutResults();
        }

        public void CreateRelation(User user, User friend, string relation = GlobalConstants.FriendRelation)
        {
            var userInDb = Get(user.UserName);
            if (userInDb == null)
            {
                Create(user);
            }

            if (Get(friend.UserName) == null)
            {
                Create(friend);
            }

            _client.Cypher
                .Match("(u:User {login: {userLogin}}), (f:User {login: {friendLogin}})")
                .WithParam("userLogin", user.UserName)
                .WithParam("friendLogin", friend.UserName)
                .Create($"(u)-[r:{relation}]->(f)")
                .ExecuteWithoutResults();
        }
        #endregion
        #region Read
        public GraphUser Get(string login)
        {
            return _client.Cypher
                .Match("(u:User {login: {userLogin}})")
                .WithParam("userLogin", login)
                .Return(u => u.As<GraphUser>()).Results.FirstOrDefault();
        }

        public GraphUser GetFriendByLogin(User user, string login)
        {
            return _client.Cypher
                .Match("(u:User {login: {userLogin}}),(f:User {login: {friendLogin}}), (u)-[Friended_With]->(f)")
                .WithParam("userLogin", user.UserName)
                .WithParam("friendLogin", login)
                .Return<GraphUser>("f").Results.FirstOrDefault();
        }

        public int PathLengthBetween(User user, User friend)
        {
            var u1 = Get(user.UserName);
            var u2 = Get(friend.UserName);
            bool isNull = false;
            if (u1 == null)
            {
                isNull = true;
                Create(user);
            }
            if (u2 == null)
            {
                isNull = true;
                Create(friend);
            }
            if (!isNull)
            {
                return _client.Cypher
                .Match("(u:User {login: {userLogin}}), (d:User {login: {destLogin}}), p=shortestPath((u)-[*..15]-(d))")
                .WithParam("userLogin", user.UserName)
                .WithParam("destLogin", friend.UserName)
                .Return<int>("length(p)").Results.FirstOrDefault();
            }
            return default;
        }
        #endregion

    }
}