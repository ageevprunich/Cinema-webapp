using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class ShowtimeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
