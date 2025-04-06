using System.Net.Sockets;
using System;

namespace Cinema_webapp.Models
{
    public class Showtime : BaseModelClass
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; } //Створити зв'язок?

        public IEnumerable<Ticket> Tickets { get; set; }
        public Movie Movie { get; set; }
    }
}
