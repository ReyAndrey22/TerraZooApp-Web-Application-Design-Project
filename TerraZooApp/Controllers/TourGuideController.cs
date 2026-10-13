using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TerraZooApp.Models;
using TerraZooApp.Data;
using Microsoft.EntityFrameworkCore;
using TerraZooApp.Services;

namespace TerraZooApp.Controllers;

[Authorize(Roles = "tour guide")]
public class TourGuideController : Controller
{
    private readonly UserManager<Users> _userManager;
    private readonly TerraZooAppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IUsersAuthService _authService;

    public TourGuideController(
        UserManager<Users> userManager, 
        TerraZooAppDbContext context,
        IWebHostEnvironment webHostEnvironment,
        IUsersAuthService authService)
    {
        _userManager = userManager;
        _context = context;
        _webHostEnvironment = webHostEnvironment;
        _authService = authService;
    }

    public async Task<IActionResult> DashboardTourGuide()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "UsersAuth");
        }

        // Get tours for this guide
        var tours = await _context.Tours
            .Where(t => t.UserId == user.Id)
            .OrderByDescending(t => t.DateTour)
            .ToListAsync();

        ViewBag.Tours = tours;
        return View();
    }

    public async Task<IActionResult> ViewProfileTourGuide()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "UsersAuth");
        }

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfileTourGuide(Users model, IFormFile ProfilePictureFile)
    {
        if (!ModelState.IsValid)
        {
            return View("ViewProfileTourGuide", model);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "UsersAuth");
        }

        var (success, message) = await _authService.UpdateProfileAsync(model, ProfilePictureFile);
        if (!success)
        {
            ModelState.AddModelError("", message);
            return View("ViewProfileTourGuide", model);
        }

        TempData["SuccessMessage"] = "Profil actualizat cu succes!";
        return RedirectToAction(nameof(ViewProfileTourGuide));
    }
}
