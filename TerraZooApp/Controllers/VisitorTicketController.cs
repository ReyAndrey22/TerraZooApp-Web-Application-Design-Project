using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TerraZooApp.Models;
using TerraZooApp.Services;
using System.Security.Claims;
using TerraZooApp.Data;
using Microsoft.EntityFrameworkCore;

namespace TerraZooApp.Controllers
{
    [Authorize(Roles = "visitor")]
    public class VisitorTicketController : Controller
    {
        private readonly ITicketTypeService _ticketTypeService;
        private readonly ITourService _tourService;
        private readonly TerraZooAppDbContext _context;

        public VisitorTicketController(
            ITicketTypeService ticketTypeService,
            ITourService tourService,
            TerraZooAppDbContext context)
        {
            _ticketTypeService = ticketTypeService;
            _tourService = tourService;
            _context = context;
        }

        public IActionResult BuyTickets()
        {
            var ticketTypes = _ticketTypeService.GetAll();
            var availableTours = _tourService.GetAllToursAsync().Result
                .Where(t => t.DateTour >= DateOnly.FromDateTime(DateTime.Now))
                .ToList();

            ViewBag.TicketTypes = ticketTypes;
            ViewBag.AvailableTours = availableTours;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseTicket(int ticketTypeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            var ticket = new Ticket
            {
                UserId = userId,
                IdTicketType = ticketTypeId,
                IdTour = null, // Biletele nu sunt asociate direct cu un tur la momentul achiziției
                PurchaseDate = DateOnly.FromDateTime(DateTime.Now)
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Ticket purchased successfully! You can now reserve a tour from your profile.";
            return RedirectToAction("BuyTickets");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveTour(int ticketId, int tourId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            var ticket = await _context.Tickets
                                     .Where(t => t.IdTicket == ticketId && t.UserId == userId)
                                     .FirstOrDefaultAsync();

            if (ticket == null)
            {
                TempData["ErrorMessage"] = "Biletul nu a fost găsit sau nu vă aparține.";
                return RedirectToAction(nameof(MyTickets));
            }

            if (ticket.IdTour != null)
            {
                TempData["ErrorMessage"] = "Acest bilet este deja asociat cu un tur.";
                return RedirectToAction(nameof(MyTickets));
            }

            var tour = await _tourService.GetTourByIdAsync(tourId);
            if (tour == null)
            {
                TempData["ErrorMessage"] = "Turul selectat nu a fost găsit.";
                return RedirectToAction(nameof(MyTickets));
            }

            if (tour.NumberOfSpots <= 0)
            {
                TempData["ErrorMessage"] = "Nu mai sunt locuri disponibile pentru acest tur.";
                return RedirectToAction(nameof(MyTickets));
            }

            ticket.IdTour = tourId; // Asociază biletul cu turul
            tour.NumberOfSpots--; // Decrementează numărul de locuri

            var (success, message) = await _tourService.UpdateTourAsync(tour);
            if (!success)
            {
                TempData["ErrorMessage"] = "Eroare la actualizarea numărului de locuri: " + message;
                return RedirectToAction(nameof(MyTickets));
            }

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Tur rezervat cu succes pentru biletul dumneavoastră!";
            return RedirectToAction(nameof(MyTickets));
        }

        public async Task<IActionResult> MyTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "UsersAuth");
            }

            var tickets = await _context.Tickets
                .Include(t => t.TicketType)
                .Include(t => t.Tour)
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.PurchaseDate)
                .ToListAsync();

            var availableTours = _tourService.GetAllToursAsync().Result
                .Where(t => t.DateTour >= DateOnly.FromDateTime(DateTime.Now) && t.NumberOfSpots > 0)
                .ToList();

            ViewBag.AvailableTours = availableTours;

            return View(tickets);
        }
    }
} 