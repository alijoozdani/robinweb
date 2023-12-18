using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using RobinWeb.Core.DTOs.BlogsViewModel;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services.Interfaces
{
    public interface IBlogService
    {
        bool AddBlog(Blog blog,IFormFile image);
        bool EditBlog(Blog blog,IFormFile image);
        BlogForFilteringViewModel GetBlogsByFilter(int pageId, int take, string search);
        List<BlogViewModel> GetLastBlogs(int take);
        Blog GetBlogById(int blogId);
        Blog GetBlogByShortLink(string shortLink);
        void AddVisitForBlog(Blog blog);
        bool DeleteBlog(int blogId);
    }
}
