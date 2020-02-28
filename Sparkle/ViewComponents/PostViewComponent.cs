using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Services;
using System.Linq;
using System.Threading.Tasks;
namespace Sparkle.ViewComponents
{
    public class PostViewComponent : ViewComponent
    {
        private readonly PostService _postService;


        public PostViewComponent(PostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string username)
        {
            var items = (await _postService.GetAsync()).OrderByDescending(p => p.CreatedDate).ToList();
            if (!string.IsNullOrEmpty(username))
            {
                items = items.Where(p => p.OwnerUserName == username).ToList();
            }

            return View(items);
        }


    }

}
