using Microsoft.EntityFrameworkCore;
using TerraZooApp.Data;
using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public class TourGuideScheduleRepository : ITourGuideScheduleRepository
    {
        private readonly TerraZooAppDbContext _context;

        public TourGuideScheduleRepository(TerraZooAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TourGuideSchedule>> GetSchedulesByUserIdAsync(string userId)
        {
            return await _context.TourGuideSchedules
                .Where(s => s.UserId == userId)
                .OrderBy(s => s.DayOfWeek)
                .ToListAsync();
        }

        public async Task<TourGuideSchedule> GetScheduleByIdAsync(int id)
        {
            return await _context.TourGuideSchedules.FindAsync(id);
        }

        public async Task<TourGuideSchedule> CreateScheduleAsync(TourGuideSchedule schedule)
        {
            _context.TourGuideSchedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<TourGuideSchedule> UpdateScheduleAsync(TourGuideSchedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task DeleteScheduleAsync(int id)
        {
            var schedule = await _context.TourGuideSchedules.FindAsync(id);
            if (schedule != null)
            {
                _context.TourGuideSchedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ScheduleExistsAsync(int id)
        {
            return await _context.TourGuideSchedules.AnyAsync(s => s.IdTourGuideSchedule == id);
        }
    }
} 