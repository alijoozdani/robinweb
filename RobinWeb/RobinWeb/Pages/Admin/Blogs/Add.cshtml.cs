using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Pages.Admin.Blogs
{
    [Authorize]
    public class AddModel : PageModel
    {
        private IBlogService _blogService;
        public AddModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [BindProperty]
        public Blog Blog { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(IFormFile imgBlogUp)
        {
            if (!ModelState.IsValid)
                return Page();

            _blogService.AddBlog(Blog, imgBlogUp);

            return RedirectToPage("Index");
        }
    }
}
