using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> GetAll();
        Animal GetById(int id);
        Animal? GetByIdWithReviews(int id);
        void Add(Animal animal);
        void Update(Animal animal);
        void Delete(int id);
        void Save();
    }
}
