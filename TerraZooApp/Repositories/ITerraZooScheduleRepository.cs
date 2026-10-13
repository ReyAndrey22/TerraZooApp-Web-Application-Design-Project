using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public interface ITerraZooScheduleRepository
    {
        List<TerraZooSchedule> GetAll();
        TerraZooSchedule? GetById(int id);
        void Create(TerraZooSchedule schedule);
        void Update(TerraZooSchedule schedule);
    }
}
