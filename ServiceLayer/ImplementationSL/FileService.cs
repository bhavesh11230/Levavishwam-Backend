using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Levavishwam_Backend.ServiceLayer.ImplementationSL
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folder)
        {
            var rootPath = _env.WebRootPath;
            var uploadFolder = Path.Combine(rootPath, "uploads", folder);

            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            var extension = Path.GetExtension(file.FileName).ToLower();
            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{folder}/{fileName}";
        }
    }
}
