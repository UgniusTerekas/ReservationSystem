using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.Review;
using Moq;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Controllers
{
    public class ReviewControllerTests
    {
        private readonly Mock<IReviewServices> _reviewServicesMock;
        private readonly ReviewController _controller;

        public ReviewControllerTests()
        {
            _reviewServicesMock = new Mock<IReviewServices>();
            _controller = new ReviewController(_reviewServicesMock.Object);
        }

        [Fact]
        public async Task GetReview_ValidId_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var expectedReview = new ReviewDto
            {
                Id = 1,
                Username = "testuser",
                Rating = 4.5,
                Description = "Test description",
                EntertainmentId = 1
            };
            _reviewServicesMock.Setup(s => s.GetReview(id)).ReturnsAsync(expectedReview);

            // Act
            var result = await _controller.GetReview(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualReview = Assert.IsType<ReviewDto>(okResult.Value);
            Assert.Equal(expectedReview.Id, actualReview.Id);
            Assert.Equal(expectedReview.Username, actualReview.Username);
            Assert.Equal(expectedReview.Rating, actualReview.Rating);
            Assert.Equal(expectedReview.Description, actualReview.Description);
            Assert.Equal(expectedReview.EntertainmentId, actualReview.EntertainmentId);
        }

        [Fact]
        public async Task GetReviews_ReturnsOkObjectResult_WhenReviewsExist()
        {
            // Arrange
            var reviews = new List<ReviewDto>()
        {
            new ReviewDto()
            {
                Id = 1,
                Username = "user1",
                Rating = 3.5,
                Description = "description1",
                EntertainmentId = 1
            },
            new ReviewDto()
            {
                Id = 2,
                Username = "user2",
                Rating = 4.2,
                Description = "description2",
                EntertainmentId = 1
            }
        };

            _reviewServicesMock.Setup(x => x.GetReviews()).ReturnsAsync(reviews);

            // Act
            var result = await _controller.GetReviews();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<ReviewDto>>(okObjectResult.Value);
            Assert.Equal(reviews.Count, resultValue.Count);
            Assert.Equal(reviews[0].Username, resultValue[0].Username);
            Assert.Equal(reviews[1].Description, resultValue[1].Description);
        }

        [Fact]
        public async Task GetReviews_ReturnsEmptyList_WhenNoReviewsExist()
        {
            // Arrange
            var reviews = new List<ReviewDto>();

            _reviewServicesMock.Setup(x => x.GetReviews()).ReturnsAsync(reviews);

            // Act
            var result = await _controller.GetReviews();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<ReviewDto>>(okObjectResult.Value);
            Assert.Empty(resultValue);
        }

        [Fact]
        public async Task GetReview_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var id = 0;
            _reviewServicesMock.Setup(s => s.GetReview(id)).ReturnsAsync((ReviewDto)null);

            // Act
            var result = await _controller.GetReview(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateReview_ValidData_ReturnsCreated()
        {
            // Arrange
            var createReviewDto = new CreateReviewDto
            {
                EntertainmentId = 4,
                Rating = 4.5,
                Description = "Great experience!"
            };
            var expectedReview = true;
            _reviewServicesMock.Setup(s => s.CreateReview(createReviewDto, 1)).ReturnsAsync(true);
            var userId = "1";
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId) }));

            // Act
            var result = await _controller.CreateReview(createReviewDto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(expectedReview, createdResult.Value);
        }

        [Fact]
        public async Task CreateReview_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var createReviewDto = new CreateReviewDto
            {
                EntertainmentId = 0,
                Rating = 5,
                Description = ""
            };
            var expectedBadRequestResult = new BadRequestResult();
            var userId = 1;
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                    }, "mock"))
                }
            };


            // Act
            var result = await _controller.CreateReview(createReviewDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(expectedBadRequestResult.StatusCode, (result as BadRequestResult).StatusCode);
        }

        [Fact]
        public async Task UpdateReview_ValidData_ReturnsNoContent()
        {
            // Arrange
            var updateReviewDto = new UpdateReviewDto
            {
                Id = 1,
                Rating = 4.5,
                Description = "Great experience!"
            };
            _reviewServicesMock.Setup(s => s.UpdateReview(updateReviewDto, 1)).ReturnsAsync(true);
            var userId = "1";
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId) }));

            // Act
            var result = await _controller.UpdateReview(updateReviewDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task UpdateReview_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var updateReviewDto = new UpdateReviewDto
            {
                Id = 0,
                Rating = 4.5,
                Description = "Great experience!"
            };
            var expectedBadRequestResult = new BadRequestObjectResult("Invalid review data");
            var userId = 1;
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                    }, "mock"))
                }
            };

            // Act
            var result = await _controller.UpdateReview(updateReviewDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            Assert.Equal(expectedBadRequestResult.StatusCode, (result as BadRequestResult).StatusCode);
        }

        [Fact]
        public async Task DeleteReview_ValidReviewId_ReturnsNoContent()
        {
            // Arrange
            int reviewId = 1;
            _reviewServicesMock.Setup(s => s.DeleteReview(reviewId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteReview(reviewId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _reviewServicesMock.Verify(s => s.DeleteReview(reviewId), Times.Once);
        }

        [Fact]
        public async Task DeleteReview_InvalidReviewId_ReturnsNotFound()
        {
            // Arrange
            int reviewId = 0;
            _reviewServicesMock.Setup(s => s.DeleteReview(reviewId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteReview(reviewId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _reviewServicesMock.Verify(s => s.DeleteReview(reviewId), Times.Once);
        }
    }
}
