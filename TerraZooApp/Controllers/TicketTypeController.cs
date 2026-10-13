using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Models;
using TerraZooApp.Services;

namespace TerraZooApp.Controllers
{
    public class TicketTypeController : Controller
    {
        private readonly ITicketTypeService _service;

        public TicketTypeController(ITicketTypeService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var ticketTypes = _service.GetAll();
            return View(ticketTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(ticketType);
                    TempData["SuccessMessage"] = "Tipul de bilet a fost creat cu succes!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "A apărut o eroare la crearea tipului de bilet: " + ex.Message);
                }
            }
            return View(ticketType);
        }

        public IActionResult Edit(int id)
        {
            var ticketType = _service.GetById(id);
            if (ticketType == null)
            {
                return NotFound();
            }
            return View(ticketType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TicketType ticketType)
        {
            if (id != ticketType.IdTicketType)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(ticketType);
                    TempData["SuccessMessage"] = "Tipul de bilet a fost actualizat cu succes!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "A apărut o eroare la actualizarea tipului de bilet: " + ex.Message);
                }
            }
            return View(ticketType);
        }
    }
} 