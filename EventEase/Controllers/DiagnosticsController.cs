using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Models;
using System.Data.SqlClient;

namespace EventEase.Controllers
{
    public class DiagnosticsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public DiagnosticsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var diagnostics = new
            {
                DatabaseConnection = await TestDatabaseConnection(),
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Not set",
                MachineName = Environment.MachineName,
                CurrentTime = DateTime.UtcNow
            };

            return View(diagnostics);
        }

        private async Task<string> TestDatabaseConnection()
        {
            try
            {
                // Test 1: Basic connection
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                await connection.CloseAsync();

                // Test 2: EF Core connection
                var canConnect = await _context.Database.CanConnectAsync();

                // Test 3: Query actual data
                var venueCount = await _context.Venues.CountAsync();
                var eventCount = await _context.Events.CountAsync();

                return $"SUCCESS: Connected to database. Venues: {venueCount}, Events: {eventCount}";
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        [HttpPost]
        public async Task<IActionResult> ApplyMigrations()
        {
            try
            {
                await _context.Database.MigrateAsync();
                return Content("Migrations applied successfully!");
            }
            catch (Exception ex)
            {
                return Content($"Migration failed: {ex.Message}");
            }
        }
    }
}