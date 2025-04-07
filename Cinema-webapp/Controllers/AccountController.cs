using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema_webapp.Data;
using System.Security.Claims;

[Authorize]
public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    //public IActionResult MyTickets()
    //{
    //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

    //    var tickets = _context.Tickets
    //        .Include(t => t.Seat)
    //        .Include(t => t.Showtime).ThenInclude(s => s.Movie)
    //        .Where(t => t.UserId == userId)
    //        .ToList();

    //    return View(tickets);
    //}

    public IActionResult MyTickets(int page = 1)
    {
        int pageSize = 10;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var userTickets = _context.Tickets
            .Include(t => t.Seat)
            .Include(t => t.Showtime).ThenInclude(s => s.Movie)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.Showtime.StartTime); // Новіші — перші

        var totalCount = userTickets.Count();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var tickets = userTickets
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View(tickets);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Refund(int ticketId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var ticket = _context.Tickets.FirstOrDefault(t => t.Id == ticketId && t.UserId == userId);

        if (ticket == null || ticket.Status != "Paid")
        {
            return NotFound(); 
        }

        ticket.Status = "Refunded"; 
        _context.SaveChanges();

        TempData["Message"] = "Квиток успішно повернено.";
        return RedirectToAction("MyTickets");
    }
}
