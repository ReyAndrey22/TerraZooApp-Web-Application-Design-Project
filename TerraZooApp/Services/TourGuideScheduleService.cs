using TerraZooApp.Models;
using TerraZooApp.Repositories;

namespace TerraZooApp.Services
{
    public class TourGuideScheduleService : ITourGuideScheduleService
    {
        private readonly ITourGuideScheduleRepository _scheduleRepository;

        public TourGuideScheduleService(ITourGuideScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task<IEnumerable<TourGuideSchedule>> GetSchedulesByUserIdAsync(string userId)
        {
            return await _scheduleRepository.GetSchedulesByUserIdAsync(userId);
        }

        public async Task<TourGuideSchedule> GetScheduleByIdAsync(int id)
        {
            return await _scheduleRepository.GetScheduleByIdAsync(id);
        }

        public async Task<TourGuideSchedule> CreateScheduleAsync(TourGuideSchedule schedule)
        {
            if (!await ValidateScheduleAsync(schedule))
            {
                throw new InvalidOperationException("Programul nu este valid.");
            }

            return await _scheduleRepository.CreateScheduleAsync(schedule);
        }

        public async Task<TourGuideSchedule> UpdateScheduleAsync(TourGuideSchedule schedule)
        {
            if (!await ValidateScheduleAsync(schedule))
            {
                throw new InvalidOperationException("Programul nu este valid.");
            }

            return await _scheduleRepository.UpdateScheduleAsync(schedule);
        }

        public async Task DeleteScheduleAsync(int id)
        {
            await _scheduleRepository.DeleteScheduleAsync(id);
        }

        public async Task<bool> ValidateScheduleAsync(TourGuideSchedule schedule)
        {
            if (schedule == null)
                return false;

            if (string.IsNullOrEmpty(schedule.DayOfWeek))
                return false;

            if (schedule.StartHour >= schedule.EndHour)
                return false;

            // Verifică dacă există suprapuneri cu alte programe în aceeași zi
            var existingSchedules = await _scheduleRepository.GetSchedulesByUserIdAsync(schedule.UserId);
            var overlappingSchedules = existingSchedules
                .Where(s => s.IdTourGuideSchedule != schedule.IdTourGuideSchedule && 
                           s.DayOfWeek == schedule.DayOfWeek &&
                           ((s.StartHour <= schedule.StartHour && s.EndHour > schedule.StartHour) ||
                            (s.StartHour < schedule.EndHour && s.EndHour >= schedule.EndHour) ||
                            (s.StartHour >= schedule.StartHour && s.EndHour <= schedule.EndHour)))
                .ToList();

            return !overlappingSchedules.Any();
        }
    }
} 