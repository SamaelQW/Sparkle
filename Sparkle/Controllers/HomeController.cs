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

        public HomeController(ILogger<HomeController> logger, PostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

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
            return RedirectToAction("Index");
        }

    }
}
