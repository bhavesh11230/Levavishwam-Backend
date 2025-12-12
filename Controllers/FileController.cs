//using Levavishwam_Backend.Dtos;
//using Levavishwam_Backend.ServiceLayer.InterfaceSL;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;

//namespace Levavishwam_Backend.Controllers
//{
//    [ApiController]
//    [Route("api/files")]
//    public class FileController : ControllerBase
//    {
//        private readonly IFileService _fileService;

//        public FileController(IFileService fileService)
//        {
//            _fileService = fileService;
//        }

//        [HttpPost("upload")]
//        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string folder)
//        {
//            var path = await _fileService.UploadFileAsync(file, folder);
//            return Ok(new FileUploadResponseDto { FilePath = path });
//        }
//    }
//}
