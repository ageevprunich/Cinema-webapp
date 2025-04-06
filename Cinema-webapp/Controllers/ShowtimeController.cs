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
        public IActionResult Index(int movieId)
        {
            var showtimes = _db.Showtimes
                .Where(s => s.MovieId == movieId)
                .ToList();

            ViewBag.MovieId = movieId;
            return View(showtimes); // View: Views/Showtime/Index.cshtml
        }

        [HttpGet]
        public IActionResult Select(int id)
        {
            var showtime = _db.Showtimes.FirstOrDefault(s => s.Id == id);
            if (showtime == null)
            {
                return NotFound();
            }

            //  Redirect з передачею showtimeId та hallId
            return RedirectToAction("Index", "Seat", new
            {
                showtimeId = showtime.Id,
                hallId = showtime.HallId
            });
        }
    }
}
