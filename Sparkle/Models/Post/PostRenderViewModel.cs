using System.Collections.Generic;
using System.Security.Claims;

namespace Sparkle.Models.Post
{
    public class PostRenderViewModel
    {
        public ClaimsPrincipal User { get; set; }

        public IEnumerable<Domain.Entities.Post> Posts { get; set; }

        public string TypeOfRender { get; set; }
    }
}
