using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using Sparkle.Models;
using System.Collections.Generic;
using System.Linq;

namespace Sparkle.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        #region Privete Members
        private readonly ILogger<HomeController> _logger;
        private readonly PostService _postService;
        private readonly UserService _userService;
        private UserProfileViewModel user;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger, PostService postService, UserService userService)
        {
            _logger = logger;
            _postService = postService;
            _userService = userService;

        }
        #endregion

        #region Action Methods

        /// <summary>
        /// The action with Posts in news stream
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/")]
        [Route("/Index")]
        public IActionResult Index()
        {
           
            return View();
        }


        /// <summary>
        /// The page where <see cref="User"/> may add new post
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/AddPost")]
        public IActionResult AddPost()
        {
            return View();
        }

        /// <summary>
        /// The action where <paramref name="post"/> will add in database
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/AddPost")]
        public IActionResult AddPost(Post post)
        {
            _postService.Create(post);
            return RedirectToAction("Index");
        }


        /// <summary>
        /// The <see cref="User"/> profile page with posts and information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Profile")]
        public IActionResult Profile()
        {

            if (user == null)
            {
                user = GetUser();
            }

            return View(user);
        }

        #endregion

        #region Private Members

        private async void UpdatePosts()
        {
            foreach (var item in _postService.Get())
            {
                await _postService.UpdateAsync(item.Id, item);
            }
            return;
            var _user = _userService.GetByUserName(User.Identity.Name);

            List<string> ids = new List<string>();
            foreach (var item in _postService.Get())
            {
                ids.Add(item.Id);

            }
            _user.PostIds = ids;
            await _userService.UpdateAsync(_user);

        }


        private UserProfileViewModel GetUser()
        {
            return new UserProfileViewModel()
            {
                User = _userService.GetByUserName(User.Identity.Name),
                Posts = _postService.Get().Where(p => p.OwnerUserName == User.Identity.Name),

            };
        }
        #endregion

    }
}
