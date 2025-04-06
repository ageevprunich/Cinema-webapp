using Cinema_webapp.Data;
using Cinema_webapp.Models;

namespace Cinema_webapp
{
    public static class ApplicationDbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            //Очистити залежності (в правильному порядку)
            context.Showtimes.RemoveRange(context.Showtimes);
            context.Seats.RemoveRange(context.Seats);
            context.Halls.RemoveRange(context.Halls);
            context.Movies.RemoveRange(context.Movies);

            context.SaveChanges();

            if (!context.Movies.Any())
            {
                var movies = new List<Movie>
                {
                    new Movie { Title = "Дюна 2", 
                        Description = "Фантастичний епос", 
                        Genre = "Sci-Fi", 
                        DurationMinutes = 155, 
                        PosterUrl = "https://upload.wikimedia.org/wikipedia/uk/3/39/%D0%94%D1%8E%D0%BD%D0%B0_%D0%A7%D0%B0%D1%81%D1%82%D0%B8%D0%BD%D0%B0_%D0%B4%D1%80%D1%83%D0%B3%D0%B0.jpg", 
                        ReleaseDate = new DateTime(2024, 3, 1), 
                        Rating = 8.4f },
                    new Movie { Title = "Оппенгеймер", 
                        Description = "Біографічна драма", 
                        Genre = "Drama", 
                        DurationMinutes = 180, 
                        PosterUrl = "https://upload.wikimedia.org/wikipedia/uk/9/9f/%D0%9E%D0%BF%D0%BF%D0%B5%D0%BD%D0%B3%D0%B5%D0%B9%D0%BC%D0%B5%D1%80_%28%D0%BF%D0%BE%D1%81%D1%82%D0%B5%D1%80%29.png", 
                        ReleaseDate = new DateTime(2023, 7, 21), 
                        Rating = 8.7f },
                    new Movie { Title = "Барбі", 
                        Description = "Комедія для всієї родини", 
                        Genre = "Comedy", 
                        DurationMinutes = 115, 
                        PosterUrl = "https://upload.wikimedia.org/wikipedia/uk/9/91/%D0%91%D0%B0%D1%80%D0%B1%D1%96_%28%D1%84%D1%96%D0%BB%D1%8C%D0%BC%29.jpg", 
                        ReleaseDate = new DateTime(2023, 7, 21), 
                        Rating = 7.1f },
                    new Movie { Title = "Місія: Нездійсненна 7", 
                        Description = "Екшн з Ітаном Гантом", 
                        Genre = "Action", 
                        DurationMinutes = 165, 
                        PosterUrl = "https://upload.wikimedia.org/wikipedia/uk/5/5b/%D0%9F%D0%BE%D1%81%D1%82%D0%B5%D1%80_%D0%B4%D0%BE_%D1%84%D1%96%D0%BB%D1%8C%D0%BC%D1%83_%C2%AB%D0%9C%D1%96%D1%81%D1%96%D1%8F_%D0%BD%D0%B5%D0%BC%D0%BE%D0%B6%D0%BB%D0%B8%D0%B2%D0%B0_%D0%A0%D0%BE%D0%B7%D0%BF%D0%BB%D0%B0%D1%82%D0%B0._%D0%A7%D0%B0%D1%81%D1%82%D0%B8%D0%BD%D0%B0_%D0%BF%D0%B5%D1%80%D1%88%D0%B0%C2%BB.jpg", 
                        ReleaseDate = new DateTime(2023, 7, 12), 
                        Rating = 7.8f }
                };
                context.Movies.AddRange(movies);
                context.SaveChanges();
            }

            if (!context.Halls.Any())
            {
                var halls = Enumerable.Range(1, 4).Select(i => new Hall
                {
                    Name = $"Зал {i}",
                    Rows = 4,
                    SeatsQuantity = 40
                }).ToList();

                context.Halls.AddRange(halls);
                context.SaveChanges();
            }

            if (!context.Seats.Any())
            {
                var halls = context.Halls.ToList();
                var seats = new List<Seat>();

                foreach (var hall in halls)
                {
                    for (int i = 1; i <= 40; i++)
                    {
                        var seat = new Seat
                        {
                            HallId = hall.Id,
                            Row = ((i - 1) / 10) + 1, 
                            SeatNumber = i,         // Послідовна нумерація: 1..40
                            Type = i > 32 ? "VIP" : "Standard" // Останній ряд (місця 33-40) — VIP
                        };
                        seats.Add(seat);
                    }
                }

                context.Seats.AddRange(seats);
                context.SaveChanges();
            }


            if (!context.Showtimes.Any())
            {
                var movies = context.Movies.ToList();
                var halls = context.Halls.ToList();
                var baseTime = new DateTime(2025, 4, 6, 10, 0, 0);
                var showtimes = new List<Showtime>();

                foreach (var movie in movies)
                {
                    for (int i = 0; i < 3; i++) // 3 сеанси
                    {
                        var hall = halls[(movie.Id + i - 1) % halls.Count];
                        var start = baseTime.AddHours(4 * i);
                        var end = start.AddMinutes(movie.DurationMinutes);

                        showtimes.Add(new Showtime
                        {
                            MovieId = movie.Id,
                            HallId = hall.Id,
                            StartTime = start,
                            EndTime = end
                        });
                    }
                }

                context.Showtimes.AddRange(showtimes);
                context.SaveChanges();
            }
        }
    }
}
