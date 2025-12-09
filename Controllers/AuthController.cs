using Levavishwam_Backend.CommonLayer.Auth;
using Levavishwam_Backend.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace Levavishwam_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class AuthController : ControllerBase
    {
        private readonly IAuthSL _authSL;

        public AuthController(IAuthSL authSL)
        {
            _authSL = authSL;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest request)
        {
            if (request == null) return BadRequest("Invalid request");

            var result = await _authSL.SignupAsync(request);
            if (result.IsSuccess) return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null) return BadRequest("Invalid request");

            var result = await _authSL.LoginAsync(request);
            if (result.IsSuccess) return Ok(result);
            return Unauthorized(result);
        }
    }
}
