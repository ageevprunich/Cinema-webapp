using Cinema_webapp.Data;
using Cinema_webapp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_webapp.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TicketController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmBooking(int showtimeId, List<int> seatIds)
        {
            var alreadyBooked = _db.Tickets
                .Where(t => t.ShowtimeId == showtimeId && seatIds.Contains(t.SeatId))
                .Select(t => t.SeatId)
                .ToList();

            var newTickets = seatIds
                .Where(id => !alreadyBooked.Contains(id))
                .Select(id => new Ticket
                {
                    ShowtimeId = showtimeId,
                    SeatId = id,
                    Status = "Reserved" 
                }).ToList();

            _db.Tickets.AddRange(newTickets);
            _db.SaveChanges();

            return RedirectToAction("Index", "Ticket");

        }

    }
}
