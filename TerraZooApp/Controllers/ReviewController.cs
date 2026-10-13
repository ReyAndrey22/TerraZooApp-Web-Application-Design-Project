using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Models;
using TerraZooApp.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TerraZooApp.Controllers
{
    [Authorize(Roles = "visitor,admin")] // Doar vizitatorii și adminii pot lăsa review-uri
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IAnimalService _animalService;
        public ReviewController(IReviewService reviewService, IAnimalService animalService)
        {
            _reviewService = reviewService;
            _animalService = animalService;
        }

        [AllowAnonymous]
        public IActionResult ListByAnimal(int animalId)
        {
            var animal = _animalService.GetById(animalId);
            if (animal == null) return NotFound();
            var reviews = _reviewService.GetReviewsByAnimal(animalId);
            ViewBag.Animal = animal;
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Add(int animalId)
        {
            var animal = _animalService.GetById(animalId);
            if (animal == null) return NotFound();
            var review = new Review { IdAnimal = animalId };
            ViewBag.Animal = animal;
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Review review)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Animal = _animalService.GetById(review.IdAnimal ?? 0);
                return View(review);
            }
            review.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            review.DateReview = DateOnly.FromDateTime(DateTime.Now);
            _reviewService.AddReview(review);
            TempData["SuccessMessage"] = "Review added successfully!";
            return RedirectToAction("ListByAnimal", new { animalId = review.IdAnimal });
        }
    }
} 