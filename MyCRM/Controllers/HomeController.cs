using Microsoft.AspNetCore.Mvc;

namespace MyCRM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
