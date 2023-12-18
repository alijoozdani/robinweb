using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RobinWeb.Core.DTOs.Products;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Pages.Admin.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private IProductService _productService;
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
        public ProductsForFilteringViewModel ProductsList { get; set; }
        public void OnGet(int pageId = 1,string search="")
        {
            ViewData["pageId"] = pageId;
            ProductsList = _productService.GetForFiltering(pageId, 10, search);
        }
        public IActionResult OnGetDeleteProduct(int productId)
        {
            var res = _productService.DeleteProduct(productId);
            return res ? Content("Deleted") : Content("Error");
        }
    }
}
