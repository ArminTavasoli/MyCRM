using Microsoft.AspNetCore.Mvc;

namespace MyCRM.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
