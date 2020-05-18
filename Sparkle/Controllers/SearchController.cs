using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Services;
using Sparkle.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Sparkle.Controllers
{

    public class SearchController : Controller
    {
        #region Private members
        private readonly UserService _userService;
        private readonly PostService _postService;

        #endregion


        #region Constructor
        public SearchController(UserService userService, PostService postService)
        {
            _userService = userService;
            _postService = postService;
        }
        #endregion


        [HttpPost]
        [Route("/Search/{searchString}")]
        public async Task<IActionResult> Search(string searchString)
        {
            var searchResult = (await _userService.GetAsync()).Where(u => (u.Name + ' ' + u.Surname).ToLower().Contains(searchString.ToLower()));

            SearchViewModel model = new SearchViewModel();
            model.User = await _userService.GetByUserNameAsync(User.Identity.Name);
            model.SearchedUsers = searchResult;


            return View(model);
        }
    }
}