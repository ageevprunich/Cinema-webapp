using Cinema_webapp.Data;
using Cinema_webapp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class SeatController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SeatController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index(int showtimeId, int hallId)
        {
            var allSeats = _db.Seats
                .Where(s => s.HallId == hallId)
                .ToList();

            var bookedSeats = _db.Tickets
                .Where(t => t.ShowtimeId == showtimeId && (t.Status == "Paid" || t.Status == "Reserved"))
                .Select(t => t.SeatId)
                .ToList();

            ViewBag.BookedSeatIds = bookedSeats;
            ViewBag.ShowtimeId = showtimeId;

            return View(allSeats); // <== передаємо всі місця як Model
        }

    }
}