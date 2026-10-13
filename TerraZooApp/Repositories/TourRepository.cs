using Microsoft.EntityFrameworkCore;
using TerraZooApp.Data;
using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly TerraZooAppDbContext _context;

        public TourRepository(TerraZooAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tour>> GetAllToursAsync()
        {
            return await _context.Tours
                .Include(t => t.User)
                .OrderByDescending(t => t.DateTour)
                .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> GetToursByGuideAsync(string guideId)
        {
            return await _context.Tours
                .Include(t => t.User)
                .Where(t => t.UserId == guideId)
                .OrderByDescending(t => t.DateTour)
                .ToListAsync();
        }

        public async Task<Tour?> GetTourByIdAsync(int id)
        {
            return await _context.Tours
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.IdTour == id);
        }

        public async Task<Tour> CreateTourAsync(Tour tour)
        {
            _context.Tours.Add(tour);
            await _context.SaveChangesAsync();
            return tour;
        }

        public async Task<Tour> UpdateTourAsync(Tour tour)
        {
            _context.Entry(tour).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tour;
        }

        public async Task<bool> DeleteTourAsync(int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
                return false;

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TourExistsAsync(int id)
        {
            return await _context.Tours.AnyAsync(t => t.IdTour == id);
        }
    }
} 