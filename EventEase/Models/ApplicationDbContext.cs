using Microsoft.EntityFrameworkCore;
using EventEase.Models;

namespace EventEase.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Venue>().ToTable("Venues");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Booking>().ToTable("Bookings");

            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Venue)
                .WithMany(v => v.Bookings)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed initial clients
            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 1,
                    ClientName = "John Smith",
                    Email = "john.smith@email.com",
                    PhoneNumber = "555-0123"
                },
                new Client
                {
                    ClientId = 2,
                    ClientName = "Sarah Johnson",
                    Email = "sarah.j@email.com",
                    PhoneNumber = "555-0456"
                },
                new Client
                {
                    ClientId = 3,
                    ClientName = "Mike Wilson",
                    Email = "mike.w@email.com",
                    PhoneNumber = "555-0789"
                }
            );

            // Seed sample venues
            modelBuilder.Entity<Venue>().HasData(
                new Venue { VenueId = 1, VenueName = "Grand Ballroom", Location = "123 Main St", Capacity = 500, ImageUrl = "https://via.placeholder.com/600x400?text=Grand+Ballroom" },
                new Venue { VenueId = 2, VenueName = "Conference Center", Location = "456 Oak Ave", Capacity = 200, ImageUrl = "https://via.placeholder.com/600x400?text=Conference+Center" },
                new Venue { VenueId = 3, VenueName = "Garden Pavilion", Location = "789 Park Rd", Capacity = 150, ImageUrl = "https://via.placeholder.com/600x400?text=Garden+Pavilion" }
            );

            // Seed sample events
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    EventName = "Tech Conference",
                    StartDate = DateTime.Now.AddDays(30),
                    EndDate = DateTime.Now.AddDays(30).AddHours(8), 
                    Description = "Annual technology conference"
                },
                new Event
                {
                    EventId = 2,
                    EventName = "Wedding Expo",
                    StartDate = DateTime.Now.AddDays(45),
                    EndDate = DateTime.Now.AddDays(45).AddHours(6), 
                    Description = "Wedding services showcase"
                },
                new Event
                {
                    EventId = 3,
                    EventName = "Corporate Gala",
                    StartDate = DateTime.Now.AddDays(60),
                    EndDate = DateTime.Now.AddDays(60).AddHours(5), 
                    Description = "Year-end corporate celebration"
                }
            );
        }
    }
}