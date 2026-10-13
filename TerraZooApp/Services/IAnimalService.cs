using TerraZooApp.Models;

namespace TerraZooApp.Services
{
    public interface IAnimalService
    {
        IEnumerable<Animal> GetAll();
        Animal GetById(int id);
        Animal? GetByIdWithReviews(int id);
        void Create(Animal animal);
        void Update(Animal animal);
        void Delete(int id);
    }
}
