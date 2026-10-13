using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public interface ITourGuideScheduleRepository
    {
        Task<IEnumerable<TourGuideSchedule>> GetSchedulesByUserIdAsync(string userId);
        Task<TourGuideSchedule> GetScheduleByIdAsync(int id);
        Task<TourGuideSchedule> CreateScheduleAsync(TourGuideSchedule schedule);
        Task<TourGuideSchedule> UpdateScheduleAsync(TourGuideSchedule schedule);
        Task DeleteScheduleAsync(int id);
        Task<bool> ScheduleExistsAsync(int id);
    }
} 