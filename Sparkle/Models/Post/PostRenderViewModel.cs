using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sparkle.Models.Post
{
    public class PostRenderViewModel
    {
        public ClaimsPrincipal User { get; set; }

        public string TypeOfRender { get; set; }
    }
}
