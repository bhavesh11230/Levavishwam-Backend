using Levavishwam_Backend.Models;
using Levavishwam_Backend.ServiceLayer.InterfaceSL;
using Microsoft.AspNetCore.Mvc;

namespace Levavishwam_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenus()
        {
            var menus = await _service.GetAllMenus();
            return Ok(menus);
        }

        [HttpPost]
        public async Task<IActionResult> AddMenu(Menu menu)
        {
            var result = await _service.AddMenu(menu);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenu(Menu menu)
        {
            var result = await _service.UpdateMenu(menu);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var success = await _service.DeleteMenu(id);
            if (!success)
                return NotFound("Menu not found");

            return Ok("Menu deleted");
        }
    }
}
