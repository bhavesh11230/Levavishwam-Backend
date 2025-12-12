//using Levavishwam_Backend.Dtos;
using Levavishwam_Backend.DTOs.Profile;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Levavishwam_Backend.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("get/{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var profile = await _profileService.GetProfileAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> Put(int userId, [FromBody] UpdateProfileRequestDto dto)
        {
            var updated = await _profileService.UpdateProfileAsync(userId, dto);
            if (!updated) return NotFound();
            return NoContent();
        }


        //Upload Img
        [HttpPost("{userId}/photo")]
        public async Task<IActionResult> UploadProfilePhoto(int userId, IFormFile file)
        {
            if (file == null) return BadRequest(new { error = "No file provided" });
            var newPath = await _profileService.UpdateProfilePhotoAsync(userId, file);
            if (string.IsNullOrEmpty(newPath)) return BadRequest(new { error = "Upload failed" });
            return Ok(new { Url = newPath });
        }

        // DELETE api/profile/photo/{userId}
        [HttpDelete("{userId}/deletePhoto")]
        public async Task<IActionResult> DeletePhoto(int userId)
        {
            var ok = await _profileService.DeleteProfilePhotoAsync(userId);
            if (!ok) return NotFound(new { message = "Profile not found or delete failed" });
            return Ok(new { success = true });
        }
    }
}
