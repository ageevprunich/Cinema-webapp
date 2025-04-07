﻿using Microsoft.AspNetCore.Authorization;
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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Refund(int ticketId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var ticket = _context.Tickets.FirstOrDefault(t => t.Id == ticketId && t.UserId == userId);

        if (ticket == null || ticket.Status != "Paid")
        {
            return NotFound(); // або View("Error")
        }

        ticket.Status = "Refunded"; // або "Canceled"
        _context.SaveChanges();

        TempData["Message"] = "Квиток успішно повернено.";
        return RedirectToAction("MyTickets");
    }
}
