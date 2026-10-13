using TerraZooApp.Models;
using TerraZooApp.Repositories;

namespace TerraZooApp.Services
{
    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;

        public TourService(ITourRepository tourRepository)
        {
            _tourRepository = tourRepository;
        }

        public async Task<IEnumerable<Tour>> GetAllToursAsync()
        {
            return await _tourRepository.GetAllToursAsync();
        }

        public async Task<IEnumerable<Tour>> GetToursByGuideAsync(string guideId)
        {
            return await _tourRepository.GetToursByGuideAsync(guideId);
        }

        public async Task<Tour?> GetTourByIdAsync(int id)
        {
            return await _tourRepository.GetTourByIdAsync(id);
        }

        public async Task<(bool success, string message, Tour? tour)> CreateTourAsync(Tour tour)
        {
            try
            {
                // Validări de bază
                if (string.IsNullOrWhiteSpace(tour.Title))
                {
                    return (false, "Titlul este obligatoriu", null);
                }

                if (tour.DateTour < DateOnly.FromDateTime(DateTime.Now))
                {
                    return (false, "Data turului nu poate fi în trecut", null);
                }

                if (tour.StartHour >= tour.EndHour)
                {
                    return (false, "Ora de început trebuie să fie înaintea orei de sfârșit", null);
                }

                if (tour.NumberOfSpots <= 0)
                {
                    return (false, "Numărul de locuri trebuie să fie mai mare decât 0", null);
                }

                if (tour.Duration <= 0)
                {
                    return (false, "Durata trebuie să fie mai mare decât 0", null);
                }

                var createdTour = await _tourRepository.CreateTourAsync(tour);
                return (true, "Tur creat cu succes", createdTour);
            }
            catch (Exception ex)
            {
                return (false, $"Eroare la crearea turului: {ex.Message}", null);
            }
        }

        public async Task<(bool success, string message)> UpdateTourAsync(Tour tour)
        {
            try
            {
                if (!await _tourRepository.TourExistsAsync(tour.IdTour))
                {
                    return (false, "Turul nu a fost găsit");
                }

                // Validări de bază
                if (string.IsNullOrWhiteSpace(tour.Title))
                {
                    return (false, "Titlul este obligatoriu");
                }

                if (tour.DateTour < DateOnly.FromDateTime(DateTime.Now))
                {
                    return (false, "Data turului nu poate fi în trecut");
                }

                if (tour.StartHour >= tour.EndHour)
                {
                    return (false, "Ora de început trebuie să fie înaintea orei de sfârșit");
                }

                if (tour.NumberOfSpots <= 0)
                {
                    return (false, "Numărul de locuri trebuie să fie mai mare decât 0");
                }

                if (tour.Duration <= 0)
                {
                    return (false, "Durata trebuie să fie mai mare decât 0");
                }

                await _tourRepository.UpdateTourAsync(tour);
                return (true, "Tur actualizat cu succes");
            }
            catch (Exception ex)
            {
                return (false, $"Eroare la actualizarea turului: {ex.Message}");
            }
        }

        public async Task<(bool success, string message)> DeleteTourAsync(int id)
        {
            try
            {
                if (!await _tourRepository.TourExistsAsync(id))
                {
                    return (false, "Turul nu a fost găsit");
                }

                var success = await _tourRepository.DeleteTourAsync(id);
                return success 
                    ? (true, "Tur șters cu succes") 
                    : (false, "Eroare la ștergerea turului");
            }
            catch (Exception ex)
            {
                return (false, $"Eroare la ștergerea turului: {ex.Message}");
            }
        }

        
    }
} 