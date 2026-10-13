using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Models;
using TerraZooApp.Services;
using Microsoft.AspNetCore.Identity;

namespace TerraZooApp.Controllers
{
    [Authorize(Roles = "tour guide,admin")]
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly UserManager<TerraZooApp.Models.Users> _userManager;

        public TourController(ITourService tourService, UserManager<TerraZooApp.Models.Users> userManager)
        {
            _tourService = tourService;
            _userManager = userManager;
        }

        // GET: /Tour
        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllToursAsync();
            return View(tours);
        }

        // GET: /Tour/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // GET: /Tour/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Tour/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tour tour)
        {
            if (!ModelState.IsValid)
            {
                return View(tour);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            tour.UserId = currentUser.Id;

            // Validări suplimentare
            if (string.IsNullOrWhiteSpace(tour.Title))
            {
                ModelState.AddModelError("Title", "Titlul este obligatoriu");
                return View(tour);
            }

            if (tour.Duration <= 0)
            {
                ModelState.AddModelError("Duration", "Durata trebuie să fie mai mare decât 0");
                return View(tour);
            }

            var (success, message, createdTour) = await _tourService.CreateTourAsync(tour);
            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, message);
            return View(tour);
        }

        // GET: /Tour/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: /Tour/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tour tour)
        {
            if (id != tour.IdTour)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(tour);
            }

            // Validări suplimentare
            if (string.IsNullOrWhiteSpace(tour.Title))
            {
                ModelState.AddModelError("Title", "Titlul este obligatoriu");
                return View(tour);
            }

            if (tour.Duration <= 0)
            {
                ModelState.AddModelError("Duration", "Durata trebuie să fie mai mare decât 0");
                return View(tour);
            }

            var (success, message) = await _tourService.UpdateTourAsync(tour);
            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, message);
            return View(tour);
        }

        // GET: /Tour/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tour = await _tourService.GetTourByIdAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: /Tour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var (success, message) = await _tourService.DeleteTourAsync(id);
            if (success)
            {
                TempData["SuccessMessage"] = message;
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = message;
            return RedirectToAction(nameof(Delete), new { id });
        }

        
    }
} 