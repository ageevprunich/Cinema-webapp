﻿namespace Cinema_webapp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public int DurationMinutes { get; set; }
        public string PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float Rating { get; set; }

        public IEnumerable<Showtime> Showtimes { get; set; }
    }
}
