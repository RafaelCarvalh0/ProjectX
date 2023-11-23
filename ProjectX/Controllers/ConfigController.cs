using Microsoft.AspNetCore.Mvc;

namespace ProjectX.Controllers
{
    public class ConfigController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}
