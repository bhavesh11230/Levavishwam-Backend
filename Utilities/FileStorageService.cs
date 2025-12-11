namespace Levavishwam_Backend.Utilities
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;
        private readonly long _maxBytes = 5 * 1024 * 1024;
        private readonly string[] _allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };

        public FileStorageService(IWebHostEnvironment env) { _env = env; }

        public async Task<string> SaveProfilePhotoAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) throw new ArgumentException("Empty file");
            if (file.Length > _maxBytes) throw new ArgumentException("File too large");
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowed.Contains(ext)) throw new ArgumentException("Invalid file type");
            var uploads = Path.Combine(_env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), "uploads", "profiles");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var fileName = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(uploads, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var relative = Path.Combine("uploads", "profiles", fileName).Replace('\\', '/');
            return "/" + relative;
        }
    }
}
