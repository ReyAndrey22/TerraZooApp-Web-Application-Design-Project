using TerraZooApp.Models;

namespace TerraZooApp.Services
{
    public interface ITourGuideScheduleService
    {
        Task<IEnumerable<TourGuideSchedule>> GetSchedulesByUserIdAsync(string userId);
        Task<TourGuideSchedule> GetScheduleByIdAsync(int id);
        Task<TourGuideSchedule> CreateScheduleAsync(TourGuideSchedule schedule);
        Task<TourGuideSchedule> UpdateScheduleAsync(TourGuideSchedule schedule);
        Task DeleteScheduleAsync(int id);
        Task<bool> ValidateScheduleAsync(TourGuideSchedule schedule);
    }
} 