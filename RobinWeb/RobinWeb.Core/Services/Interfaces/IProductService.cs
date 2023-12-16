using Microsoft.AspNetCore.Http;
using RobinWeb.Core.DTOs.Products;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        ProductsForFilteringViewModel GetForFiltering(int pageId, int take, string search);
        Product GetProductById(int productId);
        int InsertProduct(Product product);
        bool InsertProductForAdmin(Product product, IFormFile image, IFormFile demo);
        bool UpdateProduct(Product product);
        bool UpdateProductForAdmin(Product product, IFormFile image, IFormFile demo);
        bool DeleteProduct(int productId);
        void IncreaseNumberVisits(int prodcutId);
        string GenerateShortKey(int length);
        bool IsProductByShortLink(string link);
    }
}
