namespace Levavishwam_Backend.Utilities
{
    public interface IFileStorageService
    {
        //To store Image
        Task<string> SaveProfilePhotoAsync(IFormFile file);
    }
}
