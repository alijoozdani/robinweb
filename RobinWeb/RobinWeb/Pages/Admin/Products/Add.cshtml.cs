using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Pages.Admin.Products
{
    [Authorize]
    public class AddModel : PageModel
    {
        private IProductService _productService;
        public AddModel(IProductService productService)
        {
            _productService = productService;
        }
        [BindProperty]
        public Product Product { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost(IFormFile imgProductUp, IFormFile demoUp)
        {
            if (!ModelState.IsValid)
                return Page();

            _productService.InsertProductForAdmin(Product, imgProductUp, demoUp);

            return RedirectToPage("Index");
        }
    }
}
