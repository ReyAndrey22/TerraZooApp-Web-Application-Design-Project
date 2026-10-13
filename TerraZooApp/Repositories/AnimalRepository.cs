using TerraZooApp.Data;
using TerraZooApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TerraZooApp.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly TerraZooAppDbContext _context;

        public AnimalRepository(TerraZooAppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Animal> GetAll() => _context.Animals.ToList();

        public Animal GetById(int id) => _context.Animals.Find(id);

        public Animal? GetByIdWithReviews(int id)
        {
            return _context.Animals.Include(a => a.Reviews).FirstOrDefault(a => a.IdAnimal == id);
        }

        public void Add(Animal animal) => _context.Animals.Add(animal);

        public void Update(Animal animal) => _context.Animals.Update(animal);

        public void Delete(int id)
        {
            var animal = _context.Animals.Find(id);
            if (animal != null)
                _context.Animals.Remove(animal);
        }

        public void Save() => _context.SaveChanges();
    }
}
