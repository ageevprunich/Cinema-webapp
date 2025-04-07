using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class AdminPanel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
