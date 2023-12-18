using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Pages.Admin.Blogs
{
    [Authorize]
    public class EditModel : PageModel
    {
        private IBlogService _blogService;
        public EditModel(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [BindProperty]
        public Blog Blog { get; set; }
        public void OnGet(int id)
        {
            Blog = _blogService.GetBlogById(id);
        }
        public IActionResult OnPost(IFormFile? imgBlogUp)
        {
            if (!ModelState.IsValid)
                return Page();

            _blogService.EditBlog(Blog, imgBlogUp);

            return RedirectToPage("Index");
        }
    }
}
