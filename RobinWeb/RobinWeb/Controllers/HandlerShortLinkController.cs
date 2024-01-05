using Microsoft.AspNetCore.Mvc;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Web.Controllers
{
    public class HandlerShortLinkController : Controller
    {
        private IBlogService _blog;

        public HandlerShortLinkController(IBlogService blog)
        {
            _blog = blog;
        }
        [Route("/b/{shortLink}")]
        public IActionResult HandlerBlogShortLink(string shortLink)
        {
            var blog = _blog.GetBlogByShortLink(shortLink);
            if (blog == null)
            {
                return NotFound();
            }
            var host = Request.Host;
            var path = $"/ShowBlog/{blog.BlogId}";
            path = String.Join(
                "/",
                path.Split("/").Select(s => System.Net.WebUtility.UrlEncode(s))
            );

            return Redirect($"https://{host}{path}");
        }
    }
}