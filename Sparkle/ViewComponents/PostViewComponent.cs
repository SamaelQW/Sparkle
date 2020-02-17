using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Services;
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

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _postService.GetAsync();
            return View(items);
        }


    }

}
