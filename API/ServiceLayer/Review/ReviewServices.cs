using DataLayer.Entities.Review;
using DataLayer.Interfaces;
using DataLayer.Repositories.CityRepository.cs;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Review;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Review
{
    public class ReviewServices : IReviewServices
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;

        public ReviewServices(
            IReviewRepository reviewRepository,
            IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ReviewDto>> GetReviews()
        {
            var reviews = await _reviewRepository.GetReviews();

            if (reviews == null)
            {
                return null;
            }

            var reviewDto = reviews
                .Select(r => new ReviewDto
                {
                    Id = r.ReviewId,
                    Description = r.Review,
                    Rating = r.Rating,
                    Username = r.User.UserName,
                    EntertainmentId = r.EntertainmentId
                })
                .ToList();

            return reviewDto;
        }

        public async Task<ReviewDto> GetReview(int id)
        {
            var review = await _reviewRepository.GetReview(id);

            if (review == null)
            {
                return null;
            }

            var reviewDto = new ReviewDto
            {
                Id = review.ReviewId,
                Description = review.Review,
                Rating = review.Rating,
                Username = review.User.UserName,
                EntertainmentId =review.EntertainmentId
            };

            return reviewDto;
        }

        public async Task<bool> CreateReview(CreateReviewDto createReview, int id)
        {
            if (createReview.Rating == null
                && string.IsNullOrEmpty(createReview.Description))
            {
                return false;
            }

            var userEntity = await _userRepository.GetUser(id);

            if (userEntity == null)
            {
                return false;
            }

            var reviewEntity = new ReviewEntity
            {
                UserId = id,
                Rating = createReview.Rating,
                Review = createReview.Description,
                EntertainmentId = createReview.EntertainmentId
            };

            var result = await _reviewRepository.CreateReview(reviewEntity);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateReview(UpdateReviewDto updateReview, int id)
        {
            var review = await _reviewRepository.GetReview(updateReview.Id);

            if (review == null)
            {
                return false;
            }

            if (updateReview.Rating == null
                && string.IsNullOrEmpty(updateReview.Description))
            {
                return false;
            }

            var userEntity = await _userRepository.GetUser(id);

            review.ReviewId = updateReview.Id;
            review.Review = updateReview.Description;
            review.UserId = id;
            review.Rating = (double)updateReview.Rating;

            var result = _reviewRepository.UpdateReview(review);

            if (result == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteReview(int id)
        {
            var review = await _reviewRepository.GetReview(id);

            if (review == null)
            {
                return false;
            }

            await _reviewRepository.DeleteReview(review);

            return true;
        }
    }
}
