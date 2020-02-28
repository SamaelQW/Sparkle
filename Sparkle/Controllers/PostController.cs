using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Entities;
using Sparkle.Domain.Services;
using Sparkle.Models;
using System.Threading.Tasks;

namespace Sparkle.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        #region Private Members
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly CommentService _commentService;
        private Comment lastComment;
        #endregion

        public PostController(PostService postService, UserService userService, CommentService commentService)
        {
            _postService = postService;
            _userService = userService;
            _commentService = commentService;
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
            var comment = new Comment()
            {
                Body = model.Body,
                CreatedDate = model.Created,
                OwnerName = model.OwnerName,
                OwnerSurname = model.OwnerSurname,
                OwnerUserName = model.OwnerUserName
            };
            await _commentService.AddCommentAsync(postId, comment);
            lastComment = comment;
            return RedirectToAction("Index", "Post", model.PostId);
        }


        #region Helper Methods

        public PartialViewResult NewComment()
        {
            return PartialView(lastComment);
        }

        [HttpPost]
        [Route("Post/RemovePost/{postId}")]
        public async Task<IActionResult> RemovePost(string postId)
        {
            await _postService.RemoveAsync(postId);

            return RedirectToAction("Index", "Home");
        }


        #endregion


    }
}