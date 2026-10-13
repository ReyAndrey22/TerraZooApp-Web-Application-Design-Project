using TerraZooApp.Models;
using System.Collections.Generic;

namespace TerraZooApp.Services
{
    public interface IReviewService
    {
        IEnumerable<Review> GetReviewsByAnimal(int animalId);
        void AddReview(Review review);
    }
} 