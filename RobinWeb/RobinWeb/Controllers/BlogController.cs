using Microsoft.AspNetCore.Mvc;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index(int pageId=1,string search="")
        {
            var blogs = _blogService.GetBlogsByFilter(pageId, 10, search);
            return View(blogs);
        }

        [Route("ShowBlog/{blogId}")]
        public IActionResult ShowBlog(int blogId)
        {
            var blog = _blogService.GetBlogById(blogId);

            if (blog == null)
                return NotFound();

            _blogService.AddVisitForBlog(blog);
            return View(blog);
        }
    }
}
