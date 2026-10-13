using TerraZooApp.Models;
using TerraZooApp.Repositories;

namespace TerraZooApp.Services
{
    public class TerraZooScheduleService : ITerraZooScheduleService
    {
        private readonly ITerraZooScheduleRepository _repository;

        public TerraZooScheduleService(ITerraZooScheduleRepository repository)
        {
            _repository = repository;
        }

        public List<TerraZooSchedule> GetAll() => _repository.GetAll();

        public TerraZooSchedule? GetById(int id) => _repository.GetById(id);

        public void Create(TerraZooSchedule schedule) => _repository.Create(schedule);

        public void Update(TerraZooSchedule schedule) => _repository.Update(schedule);

        
    }
}
