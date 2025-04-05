using Cinema_webapp.Data;
using Cinema_webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class ShowtimeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ShowtimeController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Showtime> showtime  = _db.Showtimes.ToList();

            return View(showtime);
        }
        
    }
}
