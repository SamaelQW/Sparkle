using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using Sparkle.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sparkle.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        #region Private Members
        private readonly PostService _postService;
        private readonly UserService _userService;


        #endregion

        public PostController(PostService postService, UserService userService)
        {
            _postService = postService;
            _userService = userService;
        }


        [HttpGet]
        [Route("Post/{postId}")]
        public async Task<IActionResult> Index(string postId)
        {
            var viewModel = new PostViewModel()
            {
                Post = await _postService.GetAsync(postId),
                User = await _userService.GetByUserNameAsync(User.Identity.Name)
            };
            return View(viewModel);
        }

        [HttpPost]
        [Route("Post/{postId}")]
        public async Task<IActionResult> Index(string postId, AddCommentViewModel model)
        {
            var post = await _postService.GetAsync(model.PostId);
            if (post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }
            post.Comments.Add(new Comment()
            {
                Body = model.Body,
                CreatedDate = model.Created,
                OwnerName = model.OwnerName,
                OwnerSurname = model.OwnerSurname,
                OwnerUserName = model.OwnerUserName,

            });
            await _postService.UpdateAsync(post.Id, post);


            return RedirectToAction("Index","Post", model.PostId);
        }



    }
}