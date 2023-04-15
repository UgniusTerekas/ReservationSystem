using ModelLayer.Dto.Review;

namespace ServiceLayer.Interfaces
{
    public interface IReviewServices
    {
        Task<bool> CreateReview(CreateReviewDto createReview, int id);
        Task<bool> DeleteReview(int id);
        Task<ReviewDto> GetReview(int id);
        Task<List<ReviewDto>> GetReviews();
        Task<bool> UpdateReview(UpdateReviewDto updateReview, int id);
    }
}