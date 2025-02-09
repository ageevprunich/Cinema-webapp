namespace Cinema_webapp.Models
{
    public class Hall
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Seat> Seats { get; set; }
        public List<Showtime> Showtimes { get; set; }
    }
}
