using TerraZooApp.Models;

namespace TerraZooApp.Services
{
    public interface ITerraZooScheduleService
    {
        List<TerraZooSchedule> GetAll();
        TerraZooSchedule? GetById(int id);
        void Create(TerraZooSchedule schedule);
        void Update(TerraZooSchedule schedule);
    }
}
