using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sparkle.Domain.Utilities;
using System.Threading.Tasks;

namespace Sparkle.ViewComponents
{
    public class LoginBarViewComponent : ViewComponent
    {
        private IHttpContextAccessor _httpContext;
        private Util util;
        public LoginBarViewComponent(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            util = new Util(_httpContext);
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(util);
        }
    }
}
