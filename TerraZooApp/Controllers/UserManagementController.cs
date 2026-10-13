using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TerraZooApp.Models;
using System.Linq;

namespace TerraZooApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserManagementController : Controller
    {
        private readonly UserManager<Users> _userManager;

        public UserManagementController(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var users = _userManager.Users.ToList();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                users = users.Where(u => 
                    (u.UserName?.ToLower().Contains(searchString) ?? false) ||
                    (u.Email?.ToLower().Contains(searchString) ?? false) ||
                    (u.FirstName?.ToLower().Contains(searchString) ?? false) ||
                    (u.LastName?.ToLower().Contains(searchString) ?? false) ||
                    (u.Role?.ToLower().Contains(searchString) ?? false)
                ).ToList();
            }

            ViewBag.CurrentFilter = searchString;
            return View(users);
        }
    }
} 