using DataLayer.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface IReviewRepository
    {
        Task<ReviewEntity> CreateReview(ReviewEntity review);

        Task DeleteReview(ReviewEntity review);

        Task<ReviewEntity> GetReview(int id);

        Task<List<ReviewEntity>> GetReviews();

        Task<ReviewEntity> UpdateReview(ReviewEntity review);
    }
}
