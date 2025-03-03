namespace Cinema_webapp.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

        public IEnumerable<Ticket> Tickets { get; set; }
    }
}
