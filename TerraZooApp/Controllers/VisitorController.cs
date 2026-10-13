using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Services;
using System.Security.Claims;
using TerraZooApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace TerraZooApp.Controllers
{
    [Authorize(Roles = "visitor")]
    public class VisitorController : Controller
    {
        private readonly ITerraZooScheduleService _terraZooScheduleService;
        private readonly TerraZooAppDbContext _context;

        public VisitorController(ITerraZooScheduleService terraZooScheduleService, TerraZooAppDbContext context)
        {
            _terraZooScheduleService = terraZooScheduleService;
            _context = context;
        }

        public IActionResult DashboardVisitor()
        {
            return View();
        }

        public IActionResult ViewTerraZooSchedule()
        {
            var schedules = _terraZooScheduleService.GetAll();
            return View(schedules);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> ViewProfileVisitor()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound();
            }

            var tickets = await _context.Tickets
                .Include(t => t.TicketType)
                .Include(t => t.Tour)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.PurchaseDate)
                .ToListAsync();

            var availableTours = await _context.Tours
                .Where(t => t.DateTour >= DateOnly.FromDateTime(DateTime.Now) && t.NumberOfSpots > 0)
                .ToListAsync();

            ViewBag.Tickets = tickets;
            ViewBag.AvailableTours = availableTours;

            return View(user);
        }
    }
}
