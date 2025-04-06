using Cinema_webapp.Data;
using Cinema_webapp.Models;

namespace Cinema_webapp
{
    public static class ApplicationDbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Очистити залежності (в правильному порядку)
            //context.Showtimes.RemoveRange(context.Showtimes);
            //context.Seats.RemoveRange(context.Seats);
            //context.Halls.RemoveRange(context.Halls);
            //context.Movies.RemoveRange(context.Movies);

            //context.SaveChanges();

            if (!context.Movies.Any())
            {
                var movies = new List<Movie>
                {
                    new Movie { Title = "Дюна 2", Description = "Фантастичний епос", Genre = "Sci-Fi", DurationMinutes = 155, PosterUrl = "/images/dune2.jpg", ReleaseDate = new DateTime(2024, 3, 1), Rating = 8.4f },
                    new Movie { Title = "Оппенгеймер", Description = "Біографічна драма", Genre = "Drama", DurationMinutes = 180, PosterUrl = "/images/oppenheimer.jpg", ReleaseDate = new DateTime(2023, 7, 21), Rating = 8.7f },
                    new Movie { Title = "Барбі", Description = "Комедія для всієї родини", Genre = "Comedy", DurationMinutes = 115, PosterUrl = "/images/barbie.jpg", ReleaseDate = new DateTime(2023, 7, 21), Rating = 7.1f },
                    new Movie { Title = "Місія: Нездійсненна 7", Description = "Екшн з Ітаном Гантом", Genre = "Action", DurationMinutes = 165, PosterUrl = "/images/mission7.jpg", ReleaseDate = new DateTime(2023, 7, 12), Rating = 7.8f }
                };
                context.Movies.AddRange(movies);
                context.SaveChanges();
            }

            if (!context.Halls.Any())
            {
                var halls = Enumerable.Range(1, 4).Select(i => new Hall
                {
                    Name = $"Зал {i}",
                    Rows = 5,
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
                            Row = ((i - 1) / 8) + 1, // Для логіки залу все ще можна залишити ряд
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
