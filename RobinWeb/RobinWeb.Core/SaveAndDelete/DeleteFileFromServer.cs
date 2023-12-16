﻿using System.IO;

namespace RobinWeb.Core.SaveAndDelete
{
    public static class DeleteFileFromServer
    {
        public static void DeleteFile(string fileName, string path)
        {

            var pathDelete = Path.Combine(Directory.GetCurrentDirectory(), path,
                fileName);
            if (File.Exists(pathDelete))
            {
                File.Delete(pathDelete);
            }

        }
    }
}