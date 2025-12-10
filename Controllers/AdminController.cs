using Microsoft.AspNetCore.Mvc;

namespace Levavishwam_Backend.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
