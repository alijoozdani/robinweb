using Microsoft.AspNetCore.Http;
using RobinWeb.Core.Convertors;
using RobinWeb.Core.DTOs.Products;
using RobinWeb.Core.SaveAndDelete;
using RobinWeb.Core.Security;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Context;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services
{
    public class ProductService : IProductService
    {
        private RobinWebDBContext _context;
        public ProductService(RobinWebDBContext context)
        {
            _context = context;
        }
        public bool DeleteProduct(int productId)
        {
            Product product = GetProductById(productId);
            product.IsDelete = true;

            return UpdateProduct(product);
        }

        public Product GetProductById(int productId)
        {
            return _context.ProductsTbl.Find(productId);
        }

        public ProductsForFilteringViewModel GetForFiltering(int pageId, int take, string search)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.ProductsTbl;
        }

        public void IncreaseNumberVisits(int prodcutId)
        {
            var prodcut = GetProductById(prodcutId);
            if (prodcut != null)
            {
                prodcut.VisitCount += 1;
            }
        }

        public int InsertProduct(Product product)
        {
            _context.ProductsTbl.Add(product);
            _context.SaveChanges();
            return product.ProductId;
        }

        public bool InsertProductForAdmin(Product product, IFormFile image,IFormFile demo)
        {
            if (image == null) return false;
            if (!image.IsImage()) return false;

            var fileName = SaveFileInServer.SaveFile(image, "wwwroot/img/products");
            ImageConvertor imgResizer = new ImageConvertor();
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products/", fileName);
            string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products/thumb", fileName);
            imgResizer.Image_resize(imagePath, thumbPath, 150);

            var demoName = SaveFileInServer.SaveFile(image, "wwwroot/video/product");

            product.IsDelete = false;
            product.CreateDate = DateTime.Now;
            product.ShortLink = GenerateShortKey(4);
            product.AvatarName = fileName;
            product.DemoName = demoName;
            product.VisitCount = 0;
            InsertProduct(product);

            return true;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateProductForAdmin(Product product, IFormFile image, IFormFile demo)
        {
            if (image != null && image.IsImage())
            {
                if (product.AvatarName != "nophoto.png" && product.AvatarName != null)
                {
                    DeleteFileFromServer.DeleteFile(product.AvatarName, "wwwroot/img/products");
                    DeleteFileFromServer.DeleteFile(product.AvatarName, "wwwroot/img/products/thumb");
                }
                product.AvatarName = SaveFileInServer.SaveFile(image, "wwwroot/img/products");
                ImageConvertor imgResizer = new ImageConvertor();
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products", product.AvatarName);
                string thumbPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products/thumb", product.AvatarName);
                imgResizer.Image_resize(imagePath, thumbPath, 150);
            }
            if (demo != null)
            {
                if (product.DemoName != null)
                {
                    DeleteFileFromServer.DeleteFile(product.DemoName, "wwwroot/video/product");
                }
                product.DemoName = SaveFileInServer.SaveFile(demo, "wwwroot/video/product");
            }
            return UpdateProduct(product);
        }

        public string GenerateShortKey(int length)
        {
            //در این جا یک کلید با طول دلخواه تولید میکنیم
            var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);

            if (!IsProductByShortLink(key))
            {
                //تا زمانی که کلید ساخته شده تکراری باشد این اعملیات تکرار میشود

                key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
            }
            //در آخر یک کلید غیره تکراری با طول دلخواه ساخته شده
            return key;
        }

        public bool IsProductByShortLink(string link)
        {
            return _context.ProductsTbl.Any(p => p.ShortLink == link);
        }
    }
}
