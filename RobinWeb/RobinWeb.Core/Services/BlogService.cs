using Microsoft.AspNetCore.Http;
using RobinWeb.Core.DTOs.BlogsViewModel;
using RobinWeb.Core.SaveAndDelete;
using RobinWeb.Core.Security;
using RobinWeb.Core.Services.Interfaces;
using RobinWeb.DataLayer.Context;
using RobinWeb.DataLayer.Entities;

namespace RobinWeb.Core.Services
{
    public class BlogService : IBlogService
    {

        private RobinWebDBContext _context;

        public BlogService(RobinWebDBContext context)
        {
            _context = context;
        }

        public bool AddBlog(Blog blog, IFormFile image)
        {
            if (image == null) return false;
            if (!image.IsImage()) return false;

            var fileName = SaveFileInServer.SaveFile(image, "wwwroot/img/blogs");
            blog.IsDelete = false;
            blog.CreateDate = DateTime.Now;
            blog.ShortLink = GenerateShortKey(4);
            blog.ImageName = fileName;
            _context.Add(blog);
            return true;
        }

        public bool EditBlog(Blog blog, IFormFile image)
        {
            if (image != null)
            {
                if (!image.IsImage()) return false;

                DeleteFileFromServer.DeleteFile(blog.ImageName, "wwwroot/img/blogs");
                var fileName = SaveFileInServer.SaveFile(image, "wwwroot/img/blogs");
                blog.ImageName = fileName;
            }
            _context.Update(blog);
            return true;
        }

        public BlogForFilteringViewModel GetBlogsByFilter(int pageId, int take, string search)
        {
            IQueryable<Blog> result = _context.BlogsTbl;
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(r => r.Title.Contains(search) || r.Tags.Contains(search));
            }


            var skip = (pageId - 1) * take;
            var pageCount = (int)Math.Ceiling(result.Count() / (double)take);
            var blogs = result.OrderByDescending(b => b.CreateDate).Skip(skip).Take(take).Select(b => new BlogViewModel()
            {
                ImageName = b.ImageName,
                CreateDate = b.CreateDate,
                Title = b.Title,
                BlogId = b.BlogId,
                ShortLink = b.ShortLink,
                Description=b.ShortDescription
            }).ToList();
            var blogsModel = new BlogForFilteringViewModel()
            {
                Search = search,
                CurrentPage = pageId,
                PageCount = pageCount,
                StartPage = (pageId - 4 <= 0) ? 1 : pageId - 4,
                EndPage = (pageId + 5 > pageCount) ? pageCount : pageId + 5,
                Blogs = blogs
            };
            return blogsModel;
        }

        public List<BlogViewModel> GetLastBlogs(int take)
        {
            var blogModel = _context.BlogsTbl.OrderByDescending(b => b.CreateDate).Take(take).Select(b =>
                new BlogViewModel()
                {
                    ImageName = b.ImageName,
                    CreateDate = b.CreateDate,
                    Description=b.ShortDescription,
                    Title = b.Title,
                    BlogId = b.BlogId,
                    ShortLink = b.ShortLink
                }).ToList();
            return blogModel;
        }

        public void AddVisitForBlog(Blog blog)
        {
            blog.BlogVisit += 1;
           _context.Update(blog);
        }

        private string GenerateShortKey(int length)
        {
            //در این جا یک کلید با طول دلخواه تولید میکنیم
            var key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);

            while (GetBlogByShortLink(key) != null)
            {
                //تا زمانی که کلید ساخته شده تکراری باشد این اعملیات تکرار میشود

                key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
            }
            //در آخر یک کلید غیره تکراری با طول دلخواه ساخته شده
            return key;
        }

        public Blog GetBlogById(int blogId)
        {
            return _context.BlogsTbl.Find(blogId);
        }

        public Blog GetBlogByShortLink(string shortLink)
        {
            return _context.BlogsTbl.SingleOrDefault(b => b.ShortLink == shortLink);
        }
    }
}
