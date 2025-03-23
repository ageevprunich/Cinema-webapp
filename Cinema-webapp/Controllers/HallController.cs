using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class HallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
