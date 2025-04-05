using Cinema_webapp.Data;
using Cinema_webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Movie> movie = _db.Movies.ToList();

            return View(movie);
        }

        [HttpGet]
        public IActionResult Select(int id)
        {
            var movie = _db.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            // Переходимо до сторінки сеансів цього фільму
            return RedirectToAction("Index", "Showtime", new { movieId = id });
        }
    }
}


