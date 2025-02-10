namespace Cinema_webapp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // "Credit Card", "PayPal", "Cash"
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
