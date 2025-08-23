using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Models;

namespace EventEase.Controllers
{
    public class TestConnectionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestConnectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Test if we can connect to database
                var canConnect = await _context.Database.CanConnectAsync();
                ViewBag.CanConnect = canConnect;

                // Test if we can query venues
                var venueCount = await _context.Venues.CountAsync();
                ViewBag.VenueCount = venueCount;

                // Test if we can query events
                var eventCount = await _context.Events.CountAsync();
                ViewBag.EventCount = eventCount;

                // Test if we can query clients
                var clientCount = await _context.Clients.CountAsync();
                ViewBag.ClientCount = clientCount;

                // Test if we can query bookings
                var bookingCount = await _context.Bookings.CountAsync();
                ViewBag.BookingCount = bookingCount;

                // Test event dates structure
                var eventsWithDates = await _context.Events
                    .Where(e => e.StartDate != null && e.EndDate != null)
                    .CountAsync();
                ViewBag.EventsWithDates = eventsWithDates;

                // Test venues with images
                var venuesWithImages = await _context.Venues
                    .Where(v => v.ImageUrl != null)
                    .CountAsync();
                ViewBag.VenuesWithImages = venuesWithImages;

                ViewBag.Message = "Database connection successful!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Database connection failed: {ex.Message}";
                ViewBag.CanConnect = false;
                ViewBag.VenueCount = 0;
                ViewBag.EventCount = 0;
                ViewBag.ClientCount = 0;
                ViewBag.BookingCount = 0;
                ViewBag.EventsWithDates = 0;
                ViewBag.VenuesWithImages = 0;
            }

            return View();
        }

        // Test adding a venue with image
        public async Task<IActionResult> TestAddVenue()
        {
            try
            {
                var venue = new Venue
                {
                    VenueName = "Test Venue " + DateTime.Now.ToString("HHmmss"),
                    Location = "Test Location",
                    Capacity = 100,
                    ImageUrl = "https://via.placeholder.com/600x400?text=Test+Venue+" + DateTime.Now.ToString("HHmmss")
                };

                _context.Venues.Add(venue);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Test venue added successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error adding test venue: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // Test adding an event with dates
        public async Task<IActionResult> TestAddEvent()
        {
            try
            {
                var eventItem = new Event
                {
                    EventName = "Test Event " + DateTime.Now.ToString("HHmmss"),
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(1).AddHours(3),
                    Description = "This is a test event with proper dates"
                };

                _context.Events.Add(eventItem);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Test event added successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error adding test event: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // Test event date validation
        public async Task<IActionResult> TestInvalidEventDates()
        {
            try
            {
                var eventItem = new Event
                {
                    EventName = "Invalid Date Test " + DateTime.Now.ToString("HHmmss"),
                    StartDate = DateTime.Now.AddDays(2),
                    EndDate = DateTime.Now.AddDays(1), 
                    Description = "This should fail validation"
                };

                _context.Events.Add(eventItem);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Event added (unexpected success!)";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Expected error: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> ClearTestData()
        {
            try
            {
                
                var testVenues = _context.Venues.Where(v => v.VenueName.Contains("Test Venue"));
                _context.Venues.RemoveRange(testVenues);

                
                var testEvents = _context.Events.Where(e => e.EventName.Contains("Test Event") || e.EventName.Contains("Invalid Date Test"));
                _context.Events.RemoveRange(testEvents);

                
                var testClients = _context.Clients.Where(c => c.ClientName.Contains("Test Client"));
                _context.Clients.RemoveRange(testClients);

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Test data cleared successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error clearing test data: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> CheckSchema()
        {
            try
            {
                var eventsColumns = await _context.Database.SqlQueryRaw<string>(
                    "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Events' ORDER BY ORDINAL_POSITION"
                ).ToListAsync();

                var venuesColumns = await _context.Database.SqlQueryRaw<string>(
                    "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Venues' ORDER BY ORDINAL_POSITION"
                ).ToListAsync();

                ViewBag.EventsColumns = eventsColumns;
                ViewBag.VenuesColumns = venuesColumns;
                ViewBag.Message = "Database schema check completed!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error checking schema: {ex.Message}";
            }

            return View();
        }
    }
}