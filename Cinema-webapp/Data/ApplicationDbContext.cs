using Cinema_webapp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema_webapp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Showtime)
                .WithMany(sh => sh.Tickets)
                .HasForeignKey(t => t.ShowtimeId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SeatId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Payment)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PaymentId);

            modelBuilder.Entity<Showtime>()
                .HasOne(sh => sh.Movie)
                .WithMany(m => m.Showtimes)
                .HasForeignKey(sh => sh.MovieId);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Hall)
                .WithMany(h => h.Seats)
                .HasForeignKey(s => s.HallId);
        }

    }
}
