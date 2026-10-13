using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TerraZooApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly TerraZooAppDbContext _context;
        public AdminController(TerraZooAppDbContext context)
        {
            _context = context;
        }

        public IActionResult DashboardAdmin()
        {
            return View();
        }

        public async Task<IActionResult> ViewProfileAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}
