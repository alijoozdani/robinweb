using Microsoft.AspNetCore.Mvc;
using RobinWeb.Core.Services.Interfaces;

namespace RobinWeb.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index(int pageId=1,string search="")
        {
            var products = _productService.GetForFiltering(pageId, 10, search);
            return View(products);
        }

        [Route("ShowProduct/{productId}")]
        public IActionResult ShowProduct(int productId) 
        {
            var product = _productService.GetProductById(productId);

            if (product == null)
                return NotFound();

            _productService.IncreaseNumberVisits(productId);
            return View(product);
        }
    }
}
