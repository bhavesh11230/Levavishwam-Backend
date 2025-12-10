
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Microsoft.AspNetCore.Mvc;

namespace Levavishwam_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _service;

        public HomeController(IHomeService service)
        {
            _service = service;
        }

        [HttpGet("carousel")]
        public async Task<IActionResult> GetCarousel()
        {
            var data = await _service.GetCarouselAsync();
            return Ok(data);
        }

        [HttpGet("events")]
        public async Task<IActionResult> GetEvents()
        {
            var data = await _service.GetEventsAsync();
            return Ok(data);
        }

        [HttpGet("news")]
        public async Task<IActionResult> GetNews()
        {
            var data = await _service.GetNewsAsync();
            return Ok(data);
        }

        [HttpGet("news/{id}")]
        public async Task<IActionResult> GetNewsById(int id)
        {
            var data = await _service.GetNewsByIdAsync(id);
            if (data == null) return NotFound();

            return Ok(data);
        }



        [HttpGet("downloads")]
        public async Task<IActionResult> GetDownloads()
        {
            var data = await _service.GetDownloadsAsync();
            return Ok(data);
        }

        [HttpGet("committee")]
        public async Task<IActionResult> GetCommittee()
        {
            var data = await _service.GetCommitteeAsync();
            return Ok(data);
        }

        [HttpGet("information")]
        public async Task<IActionResult> GetInformation()
        {
            var data = await _service.GetInformationAsync();
            return Ok(data);
        }
    }
}
