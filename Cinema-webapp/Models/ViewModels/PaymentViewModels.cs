using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Cinema_webapp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cinema_webapp.Models.ViewModels
{
    public class PaymentIndexViewModel
    {
        public List<int> TicketIds { get; set; } = new();

        [BindNever]
        public List<Ticket> Tickets { get; set; }

        [Required(ErrorMessage = "Оберіть спосіб оплати")]
        [Display(Name = "Спосіб оплати")]
        public string PaymentMethod { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class PaymentConfirmationViewModel
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public List<Ticket> Tickets { get; set; }
    }
}
