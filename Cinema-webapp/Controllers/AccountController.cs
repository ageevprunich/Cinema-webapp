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

    public IActionResult MyTickets()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var tickets = _context.Tickets
            .Include(t => t.Seat)
            .Include(t => t.Showtime).ThenInclude(s => s.Movie)
            .Where(t => t.UserId == userId)
            .ToList();

        return View(tickets);
    }
}
