using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string Add(IFormFile file, string root)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString();
                string filePath = String.Concat(fileName, extension);
                string fileFullPath = Path.Combine(root, filePath);
                using (FileStream fileStream = File.Create(fileFullPath))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return filePath;
                }
            }
            return null;
        }

        public void Delete(string filePath)
        {
            if (Directory.Exists(filePath))
                Directory.Delete(filePath);
        }

        public string Update(IFormFile file, string filePath, string root)
        {
            if (Directory.Exists(filePath))
                Directory.Delete(filePath);
            return Add(file, root);
        }
    }
}
