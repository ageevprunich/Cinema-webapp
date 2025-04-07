using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Cinema_webapp.Models;        
using Cinema_webapp.Models.ViewModels;
using Cinema_webapp.Data;

public class PaymentController : Controller
{
    private readonly ApplicationDbContext _context;  // або інший контекст даних

    public PaymentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Payment/Index?ticketIds=1&ticketIds=2&...
    [HttpGet]
    public IActionResult Index(int[] ticketIds)
    {
        if (ticketIds == null || ticketIds.Length == 0)
        {
            // Якщо список квитків не передано – можна повернути помилку або редирект на список сеансів
            return BadRequest("No tickets selected for payment.");
        }

        // Завантажуємо вибрані квитки з бази даних
        List<Ticket> tickets = _context.Tickets
            .Include(t => t.Seat) // <--- обов'язково
            .Where(t => ticketIds.Contains(t.Id))
            .ToList();


        if (tickets.Count == 0)
        {
            return NotFound("Tickets not found.");
        }

        // Обчислюємо загальну суму
        decimal totalAmount = tickets.Sum(t => t.Price);

        // Формуємо модель представлення для відображення на сторінці
        var viewModel = new PaymentIndexViewModel
        {
            TicketIds = tickets.Select(t => t.Id).ToList(),
            Tickets = tickets,
            TotalAmount = totalAmount,
            PaymentMethod = null  // Користувач вибере на формі
        };

        return View(viewModel);
    }

    // POST: Payment/Confirm
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Confirm(PaymentIndexViewModel model)
    {
        // 🛠️ Ігноруємо валідацію для поля Tickets (воно не надсилається з форми)
        ModelState.Remove(nameof(model.Tickets));

        if (!ModelState.IsValid)
        {
            // Повертаємо квитки для відображення у Razor
            if (model.TicketIds != null && model.TicketIds.Count > 0)
            {
                model.Tickets = _context.Tickets
                    .Include(t => t.Seat)
                    .Where(t => model.TicketIds.Contains(t.Id))
                    .ToList();

                model.TotalAmount = model.Tickets.Sum(t => t.Price);
            }

            return View("Index", model);
        }

        // Перевірка наявності ID квитків
        if (model.TicketIds == null || model.TicketIds.Count == 0)
        {
            return BadRequest("Квитки не передані.");
        }

        // Завантажуємо квитки
        var ticketsToPay = _context.Tickets
            .Where(t => model.TicketIds.Contains(t.Id))
            .ToList();

        if (ticketsToPay.Count == 0)
        {
            return NotFound("Квитки не знайдено.");
        }

        // Створення платежу
        var payment = new Payment
        {
            PaymentDate = DateTime.Now,
            PaymentMethod = model.PaymentMethod,
            Amount = ticketsToPay.Sum(t => t.Price),
            Status = "Paid"
        };

        _context.Payments.Add(payment);
        _context.SaveChanges();

        // Прив'язуємо квитки до платежу
        foreach (var ticket in ticketsToPay)
        {
            ticket.Status = "Paid";
            ticket.PaymentId = payment.Id;
        }

        _context.SaveChanges();

        return RedirectToAction("Confirmation", new { id = payment.Id });
    }




    // GET: Payment/Confirmation/{id}
    [HttpGet]
    public IActionResult Confirmation(int id)
    {
        // Завантажуємо платіж, його квитки і їхні місця
        var payment = _context.Payments
            .Include(p => p.Tickets)
                .ThenInclude(t => t.Seat)
            .Include(p => p.Tickets)
                .ThenInclude(t => t.Showtime)
                    .ThenInclude(s => s.Movie)
            .FirstOrDefault(p => p.Id == id);



        if (payment == null)
        {
            return NotFound("Payment not found.");
        }

        var viewModel = new PaymentConfirmationViewModel
        {
            PaymentId = payment.Id,
            PaymentDate = payment.PaymentDate,
            PaymentMethod = payment.PaymentMethod,
            Amount = payment.Amount,
            Status = payment.Status,
            Tickets = payment.Tickets.ToList()
        };

        return View(viewModel);
    }
}
