using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Entities;
using System.Threading.Tasks;

namespace Sparkle.ViewComponents
{
    public class MiniCommentViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(Comment comment)
        {
            return View(comment);
        }
    }
}
