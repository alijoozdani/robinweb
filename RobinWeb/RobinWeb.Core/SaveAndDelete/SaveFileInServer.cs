using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace RobinWeb.Core.SaveAndDelete
{
    public static class SaveFileInServer
    {
        public static  string SaveFile(IFormFile inputTarget, string savePath)
        {
            var fileName = Guid.NewGuid() + inputTarget.FileName;
            var path = Path.Combine(Directory.GetCurrentDirectory(), savePath, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                 inputTarget.CopyTo(stream);
            }

            return fileName;
        }
    }
}