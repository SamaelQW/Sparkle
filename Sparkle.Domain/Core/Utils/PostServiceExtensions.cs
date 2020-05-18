using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparkle.Domain.Core.Utils
{
    public static class PostServiceExtensions
    {
        public static IEnumerable<Post> GetSelfPosts(this PostService self, User user)
        {
            return Task.Run(() => self.Get().Where(p => p.OwnerUserName == user.UserName)).Result;
        }
    }
}
