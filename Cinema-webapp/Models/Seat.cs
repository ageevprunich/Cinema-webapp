using System;

namespace Cinema_webapp.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }
        public string Type { get; set; } // "Standard", "VIP"

        public IEnumerable<Ticket> Tickets { get; set; }
        public Hall Hall { get; set; }
    }
}
