namespace Cinema_webapp.Models.ViewModels
{
    public class SeatViewModel
    {
        public int SeatId { get; set; }
        public int Row { get; set; }
        public int SeatNumber { get; set; }
        public string Type { get; set; } // Standard / VIP
        public bool IsBooked { get; set; }
    }
}