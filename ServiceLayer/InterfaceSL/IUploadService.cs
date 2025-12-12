using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Levavishwam_Backend.ServiceLayer.InterfaceSL
{
    public interface IUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string relativePath);
    }
}