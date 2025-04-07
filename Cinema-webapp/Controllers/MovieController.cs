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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            Console.WriteLine("POST Create method reached");
            Console.WriteLine($"Title: {movie.Title}");
            Console.WriteLine($"Description: {movie.Description}");
            Console.WriteLine($"Genre: {movie.Genre}");
            Console.WriteLine($"DurationMinutes: {movie.DurationMinutes}");
            Console.WriteLine($"PosterUrl: {movie.PosterUrl}");
            Console.WriteLine($"ReleaseDate: {movie.ReleaseDate}");
            Console.WriteLine($"Rating: {movie.Rating}");

            if (ModelState.IsValid)
            {
                _db.Movies.Add(movie);
                await _db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Movie was added!";
                return RedirectToAction("Index", "AdminPanel"); // Редірект на головну адмін-сторінку
            } else
            {
                Console.WriteLine("⛔ Model is NOT valid!");
                foreach (var modelState in ModelState)
                {
                    var key = modelState.Key;
                    var errors = modelState.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"❌ Error in {key}: {error.ErrorMessage}");
                    }
                }
            }

            // Якщо були помилки — відображаємо форму знову
            return View("~/Views/AdminPanel/Index.cshtml", movie);
        }
    }
}


