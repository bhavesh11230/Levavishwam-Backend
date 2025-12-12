//namespace Levavishwam_Backend.DTOs.File
//{
//    public class FileUploadResponse
//    {
//        public bool Success { get; set; }
//        public string? RelativePath { get; set; }
//        public string? PublicUrl { get; set; }
//        public string? Error { get; set; }
//    }
//}
namespace Levavishwam_Backend.Dtos
{
    public class FileUploadResponseDto
    {
        public string FilePath { get; set; }
    }
}
