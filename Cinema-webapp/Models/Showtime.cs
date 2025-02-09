using System.Net.Sockets;
using System;

namespace Cinema_webapp.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime => StartTime.AddMinutes(Movie.DurationMinutes);
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int HallId { get; set; }
        public Hall Hall { get; set; }
        public int AvailableSeats { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
