using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using System.Threading.Tasks;

namespace Sparkle.Controllers
{
    public class HomeController : Controller
    {
        #region Privete Members
        private readonly ILogger<HomeController> _logger;
        private readonly PostService _postService;

        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger, PostService postService)
        {
            _logger = logger;
            _postService = postService;
        }
        #endregion

        #region Action Methods

        [HttpGet]
        [Route("/")]
        [Route("/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/AddPost")]
        public IActionResult AddPost()
        {
            return View();
        }

        [HttpPost]
        [Route("/AddPost")]
        public IActionResult AddPost(Post post)
        {
            _postService.Create(post);
            //UpdatePosts();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Profile")]
        public IActionResult Profile()
        {
            return View();
        }

        #endregion

        #region Private Members

        private async void UpdatePosts()
        {
            foreach (var item in _postService.Get())
            {
                await _postService.UpdateAsync(item.Id, item);
            }
        }

        #endregion

    }
}
