using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Models;

namespace EventEase.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                
                ViewBag.VenueCount = await _context.Venues.CountAsync();
                ViewBag.EventCount = await _context.Events.CountAsync();
                ViewBag.BookingCount = await _context.Bookings.CountAsync();
                ViewBag.ClientCount = await _context.Clients.CountAsync(); 
            }
            catch (Exception ex)
            {
                
                ViewBag.VenueCount = 0;
                ViewBag.EventCount = 0;
                ViewBag.BookingCount = 0;
                ViewBag.ClientCount = 0; 

                _logger.LogError(ex, "Error loading dashboard statistics");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}