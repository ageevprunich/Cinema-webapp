namespace Cinema_webapp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ShowtimeId { get; set; }
        public int SeatId { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int? PaymentId { get; set; }

        public Showtime Showtime { get; set; }
        public Seat Seat { get; set; }
        public Payment Payment { get; set; }
    }
}
