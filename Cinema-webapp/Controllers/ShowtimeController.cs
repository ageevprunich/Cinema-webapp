using Cinema_webapp.Data;
using Cinema_webapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Movies = new SelectList(_db.Movies, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Showtime showtime)
        {
            // Лог значень полів
            Console.WriteLine("POST Create method reached");
            Console.WriteLine($"StartTime: {showtime.StartTime}");
            Console.WriteLine($"EndTime: {showtime.EndTime}");
            Console.WriteLine($"MovieId: {showtime.MovieId}");
            Console.WriteLine($"HallId: {showtime.HallId}");

            // Перевірка валідації
            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState is NOT valid!");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"⚠️ Field: {key}, Error: {error.ErrorMessage}");
                    }
                }

                ViewBag.Movies = new SelectList(_db.Movies, "Id", "Title", showtime.MovieId);
                return View(showtime);
            }

            // Якщо все валідно — додаємо в БД
            _db.Showtimes.Add(showtime);
            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Showtime created!";
            return RedirectToAction("Index", "Movie");
        }
    }
}
