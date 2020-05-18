using Sparkle.Domain.Entities;
using Sparkle.Neo4j.Domain.Services;

namespace Sparkle.Neo4j.Domain.Utils
{
    public static class GraphUserServiceExtensions
    {
        public static GraphUserService Add(this GraphUserService self, User user)
        {
            self.Create(user);
            return self;
        }
    }
}
