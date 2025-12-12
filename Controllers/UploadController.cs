using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace Levavishwam_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;


        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }


        [HttpPost("profile/upload/{userId}")]
        public async Task<IActionResult> UploadProfilePhoto(int userId, IFormFile file)
        {
            if (file == null) return BadRequest();
            var path = await _uploadService.UploadFileAsync(file, "profile-photos");
            if (path == null) return BadRequest();
            return Ok(new { Url = path });
        }


        //[HttpPost("news/{newsId}")]
        //public async Task<IActionResult> UploadNewsImage(int newsId, IFormFile file)
        //{
        //    if (file == null) return BadRequest();
        //    var path = await _uploadService.UploadFileAsync(file, "news-images");
        //    if (path == null) return BadRequest();
        //    return Ok(new { Url = path });
        //}


        //[HttpPost("module/{folder}")]
        //public async Task<IActionResult> UploadToModule([FromRoute] string folder, IFormFile file)
        //{
        //    if (file == null || string.IsNullOrWhiteSpace(folder)) return BadRequest();
        //    var safeFolder = folder.Trim().ToLower();
        //    var path = await _uploadService.UploadFileAsync(file, safeFolder);
        //    if (path == null) return BadRequest();
        //    return Ok(new { Url = path });
        //}
    }
}