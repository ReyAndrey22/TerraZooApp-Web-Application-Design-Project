using TerraZooApp.Models;

namespace TerraZooApp.Repositories
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<IEnumerable<Tour>> GetToursByGuideAsync(string guideId);
        Task<Tour?> GetTourByIdAsync(int id);
        Task<Tour> CreateTourAsync(Tour tour);
        Task<Tour> UpdateTourAsync(Tour tour);
        Task<bool> DeleteTourAsync(int id);
        Task<bool> TourExistsAsync(int id);
    }
} 