//using Levavishwam_Backend.Dtos;
using Levavishwam_Backend.DTOs.Profile;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Levavishwam_Backend.Controllers
{
    [ApiController]
    [Route("api/edit-profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var profile = await _profileService.GetProfileAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(int userId, [FromBody] UpdateProfileRequestDto dto)
        {
            var updated = await _profileService.UpdateProfileAsync(userId, dto);
            if (!updated) return NotFound();
            return NoContent();
        }
    }
}
