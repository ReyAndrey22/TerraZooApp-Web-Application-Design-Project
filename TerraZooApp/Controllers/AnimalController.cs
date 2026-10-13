using Microsoft.AspNetCore.Mvc;
using TerraZooApp.Models;
using TerraZooApp.Services;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;

namespace TerraZooApp.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AnimalController(IAnimalService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult IndexAnimal(string searchString)
        {
            var animals = _service.GetAll();
            
            if (!string.IsNullOrEmpty(searchString))
            {
                animals = animals.Where(a => a.NameAnimal.ToLower().Contains(searchString.ToLower())).ToList();
            }
            
            ViewBag.CurrentFilter = searchString;
            return View(animals);
        }

        public IActionResult AddAnimal()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            if (ModelState.IsValid)
            {
                // Salvează imaginea dacă a fost uploadată
                if (animal.ImageAnimalFile != null && animal.ImageAnimalFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/animals");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(animal.ImageAnimalFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        animal.ImageAnimalFile.CopyTo(stream);
                    }
                    animal.ImageAnimal = uniqueFileName;
                }
                _service.Create(animal);
                return RedirectToAction("IndexAnimal");
            }
            return View(animal);
        }

        public IActionResult UpdateAnimal(int id)
        {
            var animal = _service.GetById(id);
            if (animal == null)
                return NotFound();

            return View(animal);
        }

        [HttpPost]
        public IActionResult UpdateAnimal(Animal animal)
        {
            if (ModelState.IsValid)
            {
                // Upload nou dacă există
                if (animal.ImageAnimalFile != null && animal.ImageAnimalFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/animals");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(animal.ImageAnimalFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        animal.ImageAnimalFile.CopyTo(stream);
                    }
                    animal.ImageAnimal = uniqueFileName;
                }
                else
                {
                    // Păstrează imaginea veche dacă nu se selectează una nouă
                    var existing = _service.GetById(animal.IdAnimal);
                    animal.ImageAnimal = existing?.ImageAnimal;
                }

                _service.Update(animal);
                return RedirectToAction("IndexAnimal");
            }
            return View(animal);
        }

        public IActionResult DeleteAnimal(int id)
        {
            var animal = _service.GetById(id);
            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteAnimalConfirmed(int id)
        {
            _service.Delete(id);
            return RedirectToAction("IndexAnimal");
        }

        public IActionResult ListAnimals()
        {
            var animals = _service.GetAll();
            return View(animals); 
        }

    }
}
