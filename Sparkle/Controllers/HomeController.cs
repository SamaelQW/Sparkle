using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using Sparkle.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sparkle.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class HomeController : Controller
    {
        #region Privete Members
        private readonly ILogger<HomeController> _logger;
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly LikeService _likeService;

        private UserProfileViewModel user;
        #endregion

        #region Constructor
        public HomeController(ILogger<HomeController> logger, PostService postService, UserService userService, LikeService likeService)
        {
            _logger = logger;
            _postService = postService;
            _userService = userService;
            _likeService = likeService;
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


        /// <summary>
        /// View to edit user data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Edit")]
        public IActionResult Edit()
        {
            return View(GetUser());
        }

        [HttpPost]
        [Route("/Edit")]
        public async Task<IActionResult> Edit(UserProfileViewModel user)
        {
            User newUser = new User()
            {
                Age = user.User.Age,
                DateOfBirth = user.User.DateOfBirth,
                Email = user.User.Email,
                Name = user.User.Name,
                Password = user.User.Password,
                PostIds = user.User.PostIds,
                Status = user.User.Status,
                Surname = user.User.Surname,
                UserName = user.User.UserName,
                Id = user.User.Id,
            };

            await _userService.UpdateAsync(newUser);

            return RedirectToAction("Profile");
        }


        #endregion

        #region Helper Methods


        [Route("Home/LikePressed/{postId}")]
        public async Task<EmptyResult> LikePressed(string postId)
        {
            await _likeService.LikeAsync(postId, User.Identity.Name);
            return new EmptyResult();
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
