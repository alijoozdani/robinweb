using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.DTOs.BlogsViewModel;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Pages.Admin.Blogs
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IBlogService _blogService;
        public IndexModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public BlogForFilteringViewModel BlogsList { get; set; }
        public void OnGet(int pageId = 1,string search="")
        {
            ViewData["pageId"] = pageId;
            BlogsList = _blogService.GetBlogsByFilter(pageId, 10, search);
        }
        public IActionResult OnGetDeleteBlog(int blogId)
        {
            var res = _blogService.DeleteBlog(blogId);
            return res ? Content("Deleted") : Content("Error");
        }
    }
}
