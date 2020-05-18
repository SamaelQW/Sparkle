using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace Sparkle.ViewComponents
{
    public class PostViewComponent : ViewComponent
    {
        private readonly PostService _postService;
        private readonly UserService _userService;

        public PostViewComponent(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClaimsPrincipal user, string viewType)
        {
            var _user = await _userService.GetAsync(user);

            IEnumerable<Post> items = null;
            if (viewType == "own")
            {
                items = (await _postService.GetAsync()).OrderByDescending(p => p.CreatedDate).ToList();
                items = items.Where(p => p.OwnerUserName == _user.UserName).ToList();
            }

            if (viewType == "friends")
            {
                items = await _postService.GetFriendsPostsAsync(_user);
            }

            return View(items);
        }
    }

}
