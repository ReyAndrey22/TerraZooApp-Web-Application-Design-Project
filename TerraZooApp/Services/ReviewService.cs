using TerraZooApp.Models;
using TerraZooApp.Repositories;
using System.Collections.Generic;

namespace TerraZooApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        public ReviewService(IReviewRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Review> GetReviewsByAnimal(int animalId)
        {
            return _repository.GetReviewsByAnimal(animalId);
        }
        public void AddReview(Review review)
        {
            _repository.AddReview(review);
        }
    }
} 