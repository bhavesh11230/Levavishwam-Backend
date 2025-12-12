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

    }
}