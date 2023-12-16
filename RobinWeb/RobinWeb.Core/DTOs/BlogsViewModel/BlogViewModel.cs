using System;
using System.Collections.Generic;

namespace RobinWeb.Core.DTOs.BlogsViewModel
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public string ShortLink { get; set; }
    }
    public class BlogForFilteringViewModel
    {
        public List<BlogViewModel> Blogs { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string Search { get; set; }
    }
}