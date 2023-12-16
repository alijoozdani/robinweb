using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinWeb.Core.DTOs.Products
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageName { get; set; }
        public string Comment { get; set; }
        public string ShortLink { get; set; }
    }
    public class ProductsForFilteringViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string Search { get; set; }
    }
}
