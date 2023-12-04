using Microsoft.EntityFrameworkCore;
using ForecastFavorLib.Models;

namespace ForecastFavorApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define DbSet properties for different entity types
        public DbSet<User> Users { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<WeatherHistory> WeatherHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-one relationship between User and Preferences
            modelBuilder.Entity<User>()
                .HasOne(u => u.Preferences)
                .WithOne(p => p.User)
                .HasForeignKey<Preferences>(p => p.UserID);

            // Configure one-to-many relationship between User and CalendarEvent
            modelBuilder.Entity<User>()
                .HasMany(u => u.CalendarEvents)
                .WithOne(ce => ce.User)
                .HasForeignKey(ce => ce.UserID);

            // Configure one-to-many relationship between User and WeatherHistory
            modelBuilder.Entity<User>()
                .HasMany(u => u.WeatherHistories)
                .WithOne(wh => wh.User)
                .HasForeignKey(wh => wh.UserID);
        }
    }
}
