using Cinema_webapp.Data;
using Cinema_webapp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        [Authorize]
        public IActionResult ConfirmBooking(int showtimeId, List<int> seatIds, List<decimal> prices)
        {
            if (seatIds.Count != prices.Count)
            {
                return BadRequest("Невідповідність кількості місць і цін.");
            }

            var alreadyBooked = _db.Tickets
                .Where(t => t.ShowtimeId == showtimeId && seatIds.Contains(t.SeatId))
                .Select(t => t.SeatId)
                .ToList();

            var newTickets = new List<Ticket>();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            for (int i = 0; i < seatIds.Count; i++)
            {
                var seatId = seatIds[i];
                var price = prices[i];

                if (!alreadyBooked.Contains(seatId))
                {
                    newTickets.Add(new Ticket
                    {
                        ShowtimeId = showtimeId,
                        SeatId = seatId,
                        Price = price,
                        Status = "Reserved",
                        UserId = userId
                    });
                }
            }

            _db.Tickets.AddRange(newTickets);
            _db.SaveChanges();

            var ticketIds = newTickets.Select(t => t.Id).ToList();

            var fullTickets = _db.Tickets
                .Where(t => ticketIds.Contains(t.Id))
                .Include(t => t.Seat)
                .Include(t => t.Showtime)
                    .ThenInclude(s => s.Movie)
                .ToList();

            return View("BookingConfirmation", fullTickets);
        }


    }
}
