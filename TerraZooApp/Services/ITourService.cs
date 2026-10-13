using TerraZooApp.Models;

namespace TerraZooApp.Services
{
    public interface ITourService
    {
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<IEnumerable<Tour>> GetToursByGuideAsync(string guideId);
        Task<Tour?> GetTourByIdAsync(int id);
        Task<(bool success, string message, Tour? tour)> CreateTourAsync(Tour tour);
        Task<(bool success, string message)> UpdateTourAsync(Tour tour);
        Task<(bool success, string message)> DeleteTourAsync(int id);
    }
} 