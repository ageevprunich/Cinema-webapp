namespace Cinema_webapp.Models
{
    public class Ticket : BaseModelClass
    {
        public int Id { get; set; }

        public int ShowtimeId { get; set; }
        public Showtime Showtime { get; set; }

        public int SeatId { get; set; }
        public Seat Seat { get; set; }

        public decimal Price { get; set; }
        public string Status { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; } 


        //public int UserId { get; set; }
        public int? PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
