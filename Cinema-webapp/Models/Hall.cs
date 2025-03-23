﻿namespace Cinema_webapp.Models
{
    public class Hall : BaseModelClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int SeatsQuantity { get; set; }

        public IEnumerable<Seat> Seats { get; set; }
        public IEnumerable<Showtime> Showtimes { get; set; } // Створити зв'язок?

    }
}
