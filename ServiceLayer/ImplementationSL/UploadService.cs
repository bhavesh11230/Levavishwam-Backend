using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Levavishwam_Backend.ServiceLayer.ImplementationSL
{
    public class UploadService : IUploadService
    {
        private readonly IWebHostEnvironment _env;
        private readonly string[] _allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        private readonly long _maxBytes = 5 * 1024 * 1024;


        public UploadService(IWebHostEnvironment env)
        {
            _env = env;
        }


        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return null;
            if (file.Length > _maxBytes) return null;
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(ext)) return null;
            var uploadsRoot = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), folderName);
            if (!Directory.Exists(uploadsRoot)) Directory.CreateDirectory(uploadsRoot);
            var fileName = Guid.NewGuid().ToString("N") + ext;
            var physicalPath = Path.Combine(uploadsRoot, fileName);
            using (var stream = new FileStream(physicalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var relativePath = Path.Combine( folderName, fileName).Replace("\\", "/");

            return relativePath;
        }


        public Task<bool> DeleteFileAsync(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath)) return Task.FromResult(false);
            var webRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var trimmed = relativePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString());
            var full = Path.Combine(webRoot, trimmed);
            if (!File.Exists(full)) return Task.FromResult(false);
            File.Delete(full);
            return Task.FromResult(true);
        }
    }
}