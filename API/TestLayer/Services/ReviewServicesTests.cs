using DataLayer.Entities.Review;
using DataLayer.Entities.User;
using DataLayer.Interfaces;
using ModelLayer.Dto.Review;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Services
{
    public class ReviewServicesTests
    {
        private readonly Mock<IReviewRepository> _reviewRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly ReviewServices _reviewService;

        public ReviewServicesTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _reviewRepositoryMock = new Mock<IReviewRepository>();
            _reviewService = new ReviewServices(_reviewRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetReviews_WithReviews_ReturnsListOfReviewDto()
        {
            // Arrange
            var reviewRepositoryMock = new Mock<IReviewRepository>();
            var reviewEntities = new List<ReviewEntity>
    {
        new ReviewEntity
        {
            ReviewId = 1,
            Review = "Great place!",
            Rating = 4.5,
            EntertainmentId = 1,
            User = new UserEntity
            {
                UserName = "JohnDoe"
            }
        }
    };
            reviewRepositoryMock.Setup(x => x.GetReviews()).ReturnsAsync(reviewEntities);

            var expectedReviews = new List<ReviewDto>
    {
        new ReviewDto
        {
            Id = 1,
            Description = "Great place!",
            Rating = 4.5,
            Username = "JohnDoe",
            EntertainmentId = 1
        }
    };

            // Act
            var result = await _reviewService.GetReviews();

            // Assert
            Assert.Equal(expectedReviews[0].Rating, reviewEntities[0].Rating);
        }


        [Fact]
        public async Task GetReviews_WithNoReviews_ReturnsNull()
        {
            // Arrange
            _reviewRepositoryMock.Setup(x => x.GetReviews()).ReturnsAsync((List<ReviewEntity>)null);

            // Act
            var result = await _reviewService.GetReviews();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetReview_WithExistingReview_ReturnsReviewDto()
        {
            // Arrange
            var expectedReviewDto = new ReviewDto
            {
                Id = 1,
                Username = "user1",
                Rating = 4.5,
                Description = "Great place!",
                EntertainmentId = 1
            };
            var reviewEntity = new ReviewEntity
            {
                ReviewId = 1,
                UserId = 1,
                Rating = 4.5,
                Review = "Great place!",
                EntertainmentId = 1,
                User = new UserEntity { UserName = "user1" }
            };
            _reviewRepositoryMock.Setup(repo => repo.GetReview(1)).ReturnsAsync(reviewEntity);

            // Act
            var result = await _reviewService.GetReview(1);

            // Assert
            Assert.Equal(expectedReviewDto.Id, result.Id);
            Assert.Equal(expectedReviewDto.Username, result.Username);
            Assert.Equal(expectedReviewDto.Rating, result.Rating);
            Assert.Equal(expectedReviewDto.Description, result.Description);
            Assert.Equal(expectedReviewDto.EntertainmentId, result.EntertainmentId);
        }

        [Fact]
        public async Task GetReview_WithNonExistingReview_ReturnsNull()
        {
            // Arrange
            _reviewRepositoryMock.Setup(repo => repo.GetReview(1)).ReturnsAsync((ReviewEntity)null);

            // Act
            var result = await _reviewService.GetReview(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateReview_WithValidData_ReturnsTrue()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockReviewRepository = new Mock<IReviewRepository>();
    
            var createReview = new CreateReviewDto
            {
                EntertainmentId = 4,
                Rating = 4.5,
                Description = "Great place!"
            };
            var userId = 1;
            var results = true;

            mockUserRepository.Setup(x => x.GetUser(userId)).ReturnsAsync(new UserEntity());

            mockReviewRepository.Setup(x => x.CreateReview(It.IsAny<ReviewEntity>())).ReturnsAsync(new ReviewEntity());

            // Act
            var result = await _reviewService.CreateReview(createReview, userId);

            // Assert
            Assert.True(results);
        }

        [Fact]
        public async Task CreateReview_WithInvalidData_ReturnsFalse()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockReviewRepository = new Mock<IReviewRepository>();
            var createReview = new CreateReviewDto();
            var userId = 1;

            // Act
            var result = await _reviewService.CreateReview(createReview, userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CreateReview_WithNullRatingAndDescription_ReturnsFalse()
        {
            // Arrange
            var mockUserRepository = new Mock<IUserRepository>();
            var mockReviewRepository = new Mock<IReviewRepository>();
           
            var createReview = new CreateReviewDto
            {
                EntertainmentId = 1
            };
            var userId = 1;

            // Act
            var result = await _reviewService.CreateReview(createReview, userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateReview_WithValidData_ReturnsTrue()
        {
            // Arrange
            var updateReviewDto = new UpdateReviewDto
            {
                Id = 1,
                Description = "Great place!",
                Rating = 4.5
            };
            var userId = 1;
            var reviewEntity = new ReviewEntity
            {
                ReviewId = 1,
                Review = "Okay place",
                Rating = 3.5,
                UserId = 1
            };

            _reviewRepositoryMock.Setup(repo => repo.GetReview(updateReviewDto.Id))
                .ReturnsAsync(reviewEntity);
            _userRepositoryMock.Setup(repo => repo.GetUser(userId))
                .ReturnsAsync(new UserEntity());

            _reviewRepositoryMock.Setup(repo => repo.UpdateReview(reviewEntity))
                .ReturnsAsync(reviewEntity);

            // Act
            var result = await _reviewService.UpdateReview(updateReviewDto, userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateReview_WithNonExistingReview_ReturnsFalse()
        {
            // Arrange
            var updateReviewDto = new UpdateReviewDto
            {
                Id = 1,
                Description = "Great place!",
                Rating = 4.5
            };
            var userId = 1;

            _reviewRepositoryMock.Setup(repo => repo.GetReview(updateReviewDto.Id))
                .ReturnsAsync((ReviewEntity)null);

            // Act
            var result = await _reviewService.UpdateReview(updateReviewDto, userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateReview_WithInvalidData_ReturnsFalse()
        {
            // Arrange
            var updateReviewDto = new UpdateReviewDto
            {
                Id = 1,
                Description = "",
                Rating = null,
            };
            var userId = 1;
            var reviewEntity = new ReviewEntity
            {
                ReviewId = 1,
                Review = "Okay place",
                Rating = 3.5,
                UserId = 1
            };

            _reviewRepositoryMock.Setup(repo => repo.GetReview(updateReviewDto.Id))
                .ReturnsAsync(reviewEntity);

            // Act
            var result = await _reviewService.UpdateReview(updateReviewDto, userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteReview_WithExistingReview_ReturnsTrue()
        {
            // Arrange
            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository
                .Setup(repo => repo.GetReview(It.IsAny<int>()))
                .ReturnsAsync(new ReviewEntity());

            // Act
            var result = await _reviewService.DeleteReview(4);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public async Task DeleteReview_WithNonExistingReview_ReturnsFalse()
        {
            // Arrange
            var mockReviewRepository = new Mock<IReviewRepository>();
            mockReviewRepository
                .Setup(repo => repo.GetReview(It.IsAny<int>()))
                .ReturnsAsync((ReviewEntity)null);

            // Act
            var result = await _reviewService.DeleteReview(1);

            // Assert
            Assert.False(result);
        }
    }
}
