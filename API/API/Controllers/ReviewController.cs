using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Review;
using ServiceLayer.CityService;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        [HttpGet("reviews")]
        [ProducesResponseType(typeof(List<ReviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviews()
        {
            var result = await _reviewServices.GetReviews();

            return Ok(result);
        }

        [HttpGet("review")]
        [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReview(int id)
        {
            var result = await _reviewServices.GetReview(id);

            return Ok(result);
        }

        [Authorize]
        [HttpPost("review")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateReview(CreateReviewDto createReview)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _reviewServices.CreateReview(createReview, id);

            return Created(string.Empty, result);
        }

        [HttpPatch("review")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto updateReview)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _reviewServices.UpdateReview(updateReview, id);

            return Ok(result);
        }

        [HttpDelete("review")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            var result = await _reviewServices.DeleteReview(reviewId);

            return Ok(result);
        }
    }
}
