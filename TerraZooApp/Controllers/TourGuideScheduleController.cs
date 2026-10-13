using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TerraZooApp.Models;
using TerraZooApp.Services;

namespace TerraZooApp.Controllers
{
    [Authorize(Roles = "admin, tour guide")]
    public class TourGuideScheduleController : Controller
    {
        private readonly ITourGuideScheduleService _scheduleService;
        private readonly UserManager<Users> _userManager;

        public TourGuideScheduleController(
            ITourGuideScheduleService scheduleService,
            UserManager<Users> userManager)
        {
            _scheduleService = scheduleService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string userId = null)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            // If userId is provided and current user is admin, show that user's schedule
            if (!string.IsNullOrEmpty(userId) && User.IsInRole("admin"))
            {
                var targetUser = await _userManager.FindByIdAsync(userId);
                if (targetUser == null)
                {
                    return NotFound();
                }
                ViewBag.TargetUser = targetUser;
                var schedules = await _scheduleService.GetSchedulesByUserIdAsync(userId);
                return View(schedules);
            }

            // Otherwise show current user's schedule
            var userSchedules = await _scheduleService.GetSchedulesByUserIdAsync(currentUser.Id);
            return View(userSchedules);
        }

        public IActionResult Create(string userId = null)
        {
            if (!string.IsNullOrEmpty(userId) && User.IsInRole("admin"))
            {
                ViewBag.UserId = userId;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TourGuideSchedule schedule)
        {
            if (!ModelState.IsValid)
            {
                return View(schedule);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            // If userId is provided and current user is admin, use that userId
            if (!string.IsNullOrEmpty(schedule.UserId) && User.IsInRole("admin"))
            {
                var targetUser = await _userManager.FindByIdAsync(schedule.UserId);
                if (targetUser == null)
                {
                    return NotFound();
                }
            }
            else
            {
                schedule.UserId = currentUser.Id;
            }

            try
            {
                await _scheduleService.CreateScheduleAsync(schedule);
                TempData["SuccessMessage"] = "Program adăugat cu succes!";
                return RedirectToAction(nameof(Index), new { userId = schedule.UserId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(schedule);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            // Only allow if user is admin or owns the schedule
            if (!User.IsInRole("admin") && schedule.UserId != currentUser.Id)
            {
                return Forbid();
            }

            return View(schedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TourGuideSchedule schedule)
        {
            if (id != schedule.IdTourGuideSchedule)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(schedule);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            // Get the existing entity from the database
            var existingSchedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (existingSchedule == null)
            {
                return NotFound();
            }

            // Only allow if user is admin or owns the schedule
            if (!User.IsInRole("admin") && existingSchedule.UserId != currentUser.Id)
            {
                return Forbid();
            }

            // Update only the editable properties
            existingSchedule.DayOfWeek = schedule.DayOfWeek;
            existingSchedule.StartHour = schedule.StartHour;
            existingSchedule.EndHour = schedule.EndHour;

            try
            {
                await _scheduleService.UpdateScheduleAsync(existingSchedule);
                TempData["SuccessMessage"] = "Program actualizat cu succes!";
                return RedirectToAction(nameof(Index), new { userId = existingSchedule.UserId });
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(schedule);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            // Only allow if user is admin or owns the schedule
            if (!User.IsInRole("admin") && schedule.UserId != currentUser.Id)
            {
                return Forbid();
            }

            await _scheduleService.DeleteScheduleAsync(id);
            TempData["SuccessMessage"] = "Program șters cu succes!";
            return RedirectToAction(nameof(Index), new { userId = schedule.UserId });
        }
    }
} 