using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Models;
using TerraZooApp.Services;

namespace TerraZooApp.Controllers
{
    public class TerraZooScheduleController : Controller
    {
        private readonly ITerraZooScheduleService _service;

        public TerraZooScheduleController(ITerraZooScheduleService service)
        {
            _service = service;
        }

        public IActionResult IndexTerraZooSchedule()
        {
            var schedules = _service.GetAll();
            return View(schedules);
        }

        public IActionResult AddTerraZooSchedule()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddTerraZooSchedule(TerraZooSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                _service.Create(schedule);
                return RedirectToAction("IndexTerraZooSchedule");
            }
            return View(schedule);
        }

        public IActionResult UpdateTerraZooSchedule(int id)
        {
            var schedule = _service.GetById(id);
            if (schedule == null)
                return NotFound();

            return View(schedule);
        }

        [HttpPost]
        public IActionResult UpdateTerraZooSchedule(TerraZooSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                _service.Update(schedule);
                return RedirectToAction("IndexTerraZooSchedule");
            }
            return View(schedule);
        }

    }
}
