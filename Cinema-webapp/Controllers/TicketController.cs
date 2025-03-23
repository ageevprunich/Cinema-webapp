using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
