using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Tour_API.Models;

namespace Tour_API.Data
{
    public class TourContext : DbContext
    {
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public TourContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Destination>().HasData(
                new Destination { 
                    Id = 1, 
                    Name = "Paris", 
                    Image = "paris.jpg", 
                    Description = "The city of love"
                },

                new Destination { 
                    Id = 2, 
                    Name = "Tokyo", 
                    Image = "tokyo.jpg", 
                    Description = "Vibrant and bustling metropolis"
                },

                new Destination
                {
                    Id = 3,
                    Name = "New York",
                    Image = "new-york.jpg",
                    Description = "The Big Apple"
                },

                new Destination
                {
                    Id = 4,
                    Name = "London",
                    Image = "london.jpg",
                    Description = "Historical and cultural capital"
                }
            );
        }
    }
}
