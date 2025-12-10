using Microsoft.AspNetCore.Mvc;

namespace Levavishwam_Backend.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
