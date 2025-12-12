namespace Levavishwam_Backend.ServiceLayer.InterfaceSL
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folder);
    }
}
