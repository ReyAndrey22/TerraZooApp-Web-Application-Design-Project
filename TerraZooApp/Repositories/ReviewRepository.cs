using TerraZooApp.Models;
using TerraZooApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace TerraZooApp.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TerraZooAppDbContext _context;
        public ReviewRepository(TerraZooAppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Review> GetReviewsByAnimal(int animalId)
        {
            return _context.Reviews.Where(r => r.IdAnimal == animalId).ToList();
        }
        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
    }
} 