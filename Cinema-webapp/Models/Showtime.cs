using System.Net.Sockets;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema_webapp.Models
{
    public class Showtime : BaseModelClass
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int MovieId { get; set; }

        public int HallId { get; set; }

        public Movie? Movie { get; set; }
        public IEnumerable<Ticket>? Tickets { get; set; }
    }
}