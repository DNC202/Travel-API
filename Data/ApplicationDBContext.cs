using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tour_API.Models;

namespace Tour_API.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Tour> Tours { get; set; }

        public ApplicationDBContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<Destination>().HasData(
                new Destination
                {
                    Id = 1,
                    Name = "Paris",
                    Image = "paris.jpg",
                    Description = "The city of love"
                },

                new Destination
                {
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
            modelBuilder.Entity<Tour>().HasData(
                new Tour
                {
                    Id = 1,
                    Title = "Adventure in the Alps",
                    DestinationId = 3,
                    Rating = 4.8,
                    Price = 119.99,
                    Duration = "4 days 3 nights",
                    Thumbnail = "tour-1.jpg"
                },
                new Tour
                {
                    Id = 2,
                    Title = "Himalayan Escape",
                    DestinationId = 4,
                    Rating = 4.5,
                    Price = 89.99,
                    Duration = "6 days 5 nights",
                    Thumbnail = "tour-1.jpg"
                },
                new Tour
                {
                    Id = 3,
                    Title = "Rocky Mountain Adventure",
                    DestinationId = 2,
                    Rating = 4.9,
                    Price = 109.99,
                    Duration = "3 days 2 nights",
                    Thumbnail = "tour-1.jpg"
                },
                new Tour
                {
                    Id = 4,
                    Title = "Serene Mountain Journey",
                    DestinationId = 1,
                    Rating = 4.6,
                    Price = 129.99,
                    Duration = "7 days 6 nights",
                    Thumbnail = "tour-1.jpg"
                },
                new Tour
                {
                    Id = 5,
                    Title = "Peak Exploration",
                    DestinationId = 2,
                    Rating = 4.7,
                    Price = 99.99,
                    Duration = "5 days 4 nights",
                    Thumbnail = "tour-1.jpg"
                },
                new Tour
                {
                    Id = 6,
                    Title = "Highland Trails",
                    DestinationId = 4,
                    Rating = 4.5,
                    Price = 139.99,
                    Duration = "6 days 5 nights",
                    Thumbnail = "tour-1.jpg"
                },
                new Tour
                {
                    Id = 7,
                    Title = "Summit Challenge",
                    DestinationId = 1,
                    Rating = 4.8,
                    Price = 149.99,
                    Duration = "5 days 4 nights",
                    Thumbnail = "tour-1.jpg"
                },
                new Tour
                {
                    Id = 8,
                    Title = "Alpine Discovery",
                    DestinationId = 3,
                    Rating = 4.9,
                    Price = 119.99,
                    Duration = "7 days 6 nights",
                    Thumbnail = "tour-1.jpg"
                }
            );
        }
    }
}
