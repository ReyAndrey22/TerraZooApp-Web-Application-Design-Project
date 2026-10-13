using TerraZooApp.Models;
using System.Collections.Generic;

namespace TerraZooApp.Repositories
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviewsByAnimal(int animalId);
        void AddReview(Review review);
    }
} 