using DataLayer.Entities.City;
using DataLayer.Entities.Review;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Dto.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Review
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataBaseContext _dbContext;

        public ReviewRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ReviewEntity>> GetReviews()
        {
            return await _dbContext
                .Reviews
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<ReviewEntity> GetReview(int id)
        {
            return await _dbContext
                .Reviews
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ReviewId == id);
        }

        public async Task<ReviewEntity> CreateReview(ReviewEntity review)
        {
            _dbContext.Reviews.Add(review);

            await _dbContext.SaveChangesAsync();

            return review;
        }

        public async Task<ReviewEntity> UpdateReview(ReviewEntity review)
        {
            _dbContext.Entry(review).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return review;
        }

        public async Task DeleteReview(ReviewEntity review)
        {
            _dbContext.Reviews.Remove(review);

            await _dbContext.SaveChangesAsync();
        }
    }
}
