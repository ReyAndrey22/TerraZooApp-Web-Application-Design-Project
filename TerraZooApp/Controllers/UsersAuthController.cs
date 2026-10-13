using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Models;
using TerraZooApp.Services;
using Microsoft.AspNetCore.Identity;

namespace TerraZooApp.Controllers
{
    public class UsersAuthController : Controller
    {
        private readonly IUsersAuthService _authService;
        private readonly UserManager<Users> _userManager;

        public UsersAuthController(IUsersAuthService authService, UserManager<Users> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var (success, role) = await _authService.LoginAsync(email, password);
            if (!success)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View();
            }

            // Redirect based on role
            return role.ToLower() switch
            {
                "admin" => RedirectToAction("DashboardAdmin", "Admin"),
                "tour guide" => RedirectToAction("DashboardTourGuide", "TourGuide"),
                _ => RedirectToAction("DashboardVisitor", "Visitor")
            };
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users user, string password)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var (success, message) = await _authService.RegisterAsync(user, password);
            if (!success)
            {
                ModelState.AddModelError("", message);
                return View(user);
            }

            TempData["SuccessMessage"] = "Cont creat cu succes! Te rugăm să te loghezi.";
            return RedirectToAction("Login", "UsersAuth");
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login", "UsersAuth");
        }

        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login");
            }

            var currentUser = await _userManager.FindByIdAsync(user.Id);
            if (currentUser == null)
            {
                return RedirectToAction("Login");
            }

            return View(currentUser);
        }

        [HttpPost]
        public async Task<IActionResult> ViewProfile(Users model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login");

            var (success, message) = await _authService.UpdateProfileAsync(model, model.ProfilePictureFile);
            if (!success)
            {
                ModelState.AddModelError("", message);
                return View(model);
            }

            
            var updatedUser = await _userManager.FindByIdAsync(user.Id);
            if (updatedUser == null)
                return RedirectToAction("Login");

            ViewBag.Success = message;

            
            if (User.IsInRole("admin"))
            {
                return View("~/Views/Admin/ViewProfileAdmin.cshtml", updatedUser);
            }
            else if (User.IsInRole("tour guide"))
            {
                return View("~/Views/TourGuide/ViewProfileTourGuide.cshtml", updatedUser);
            }
            else if (User.IsInRole("visitor"))
            {
                return View("~/Views/Visitor/ViewProfileVisitor.cshtml", updatedUser);
            }
            else
            {
                return View(updatedUser);
            }
        }
    }
} 