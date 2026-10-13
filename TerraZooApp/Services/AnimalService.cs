using TerraZooApp.Models;
using TerraZooApp.Repositories;

namespace TerraZooApp.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _repository;

        public AnimalService(IAnimalRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Animal> GetAll() => _repository.GetAll();
        public Animal GetById(int id) => _repository.GetById(id);
        public Animal? GetByIdWithReviews(int id)
        {
            return _repository.GetByIdWithReviews(id);
        }

        public void Create(Animal animal)
        {
            _repository.Add(animal);
            _repository.Save();
        }

        public void Update(Animal animal)
        {
            var existing = _repository.GetById(animal.IdAnimal);
            if (existing == null) return;

            // Actualizează doar proprietățile dorite
            existing.NameAnimal = animal.NameAnimal;
            existing.Species = animal.Species;
            existing.ScientificName = animal.ScientificName;
            existing.Breed = animal.Breed;
            existing.Sex = animal.Sex;
            existing.Age = animal.Age;
            existing.NaturalHabitat = animal.NaturalHabitat;
            existing.DietType = animal.DietType;
            existing.ImageAnimal = animal.ImageAnimal;

            _repository.Update(existing);
            _repository.Save();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _repository.Save();
        }
    }
}
