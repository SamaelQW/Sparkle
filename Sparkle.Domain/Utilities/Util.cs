using Microsoft.AspNetCore.Http;

namespace Sparkle.Domain.Utilities
{
    public class Util
    {

        private static IHttpContextAccessor _httpContext;
        public Util(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public bool IfAuthorize()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
