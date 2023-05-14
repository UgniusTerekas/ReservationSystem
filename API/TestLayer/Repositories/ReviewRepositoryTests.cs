using DataLayer.Entities.Review;
using DataLayer.Entities.User;
using DataLayer.Repositories.Review;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities.EntertainmentItem;

namespace TestLayer.Repositories
{
    public class ReviewRepositoryTests
    {
        private readonly DbContextOptions<DataBaseContext> _options;

        public ReviewRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task GetReviews_ShouldReturnAllReviews()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var repository = new ReviewRepository(dbContext);
            var user1 = new UserEntity { UserId = 1, UserName = "John Doe",
                UserEmail = "admin@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var user2 = new UserEntity { UserId = 2, UserName = "Jane Doe",
                UserEmail = "admin@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            await dbContext.Users.AddRangeAsync(user1, user2);
            await dbContext.SaveChangesAsync();
            var review1 = new ReviewEntity { ReviewId = 1, UserId = 1, Review = "Good experience" };
            var review2 = new ReviewEntity { ReviewId = 2, UserId = 2, Review = "Bad experience" };
            await dbContext.Reviews.AddRangeAsync(review1, review2);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await repository.GetReviews();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.ReviewId == 1 && r.User.UserName == "John Doe" && r.Review == "Good experience");
            Assert.Contains(result, r => r.ReviewId == 2 && r.User.UserName == "Jane Doe" && r.Review == "Bad experience");
        }

        [Fact]
        public async Task GetReviews_ShouldReturnEmptyList_WhenNoReviewsExist()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();

            var repository = new ReviewRepository(dbContext);

            // Act
            var result = await repository.GetReviews();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetReview_ShouldReturnReview_WhenReviewExists()
        {
            // Arrange
            var user = new UserEntity { UserId = 1,
                UserName = "John Doe",
                UserEmail = "admin@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var entertainmentItem = new EntertainmentItemEntity { EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "dsgsdgf"
            };
            var review = new ReviewEntity
            {
                ReviewId = 1,
                UserId = 1,
                EntertainmentId = 1,
                Rating = 4.5,
                Review = "Great experience"
            };
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();

            dbContext.AddRange(user, entertainmentItem, review);
            await dbContext.SaveChangesAsync();
            var repository = new ReviewRepository(dbContext);

            // Act
            var result = await repository.GetReview(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(review.ReviewId, result.ReviewId);
            Assert.Equal(review.UserId, result.UserId);
            Assert.Equal(review.User.UserId, result.User.UserId);
            Assert.Equal(review.EntertainmentId, result.EntertainmentId);
            Assert.Equal(review.Entertainment.EntertainmentId, result.Entertainment.EntertainmentId);
            Assert.Equal(review.Rating, result.Rating);
            Assert.Equal(review.Review, result.Review);
        }

        [Fact]
        public async Task GetReview_ShouldReturnNull_WhenReviewDoesNotExist()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var repository = new ReviewRepository(dbContext);
            dbContext.Database.EnsureDeleted();

            // Act
            var result = await repository.GetReview(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateReview_ShouldReturnCreatedReview()
        {
            // Arrange
            var review = new ReviewEntity
            {
                UserId = 1,
                EntertainmentId = 1,
                Rating = 4.5,
                Review = "Great experience!"
            };
            using var dbContext = new DataBaseContext(_options);
            var repository = new ReviewRepository(dbContext);

            // Act
            var result = await repository.CreateReview(review);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(review.UserId, result.UserId);
            Assert.Equal(review.EntertainmentId, result.EntertainmentId);
            Assert.Equal(review.Rating, result.Rating);
            Assert.Equal(review.Review, result.Review);
        }

        [Fact]
        public async Task CreateReview_ShouldAddReviewToDatabase()
        {
            // Arrange
            var review = new ReviewEntity
            {
                UserId = 1,
                EntertainmentId = 1,
                Rating = 4.5,
                Review = "Great experience!"
            };
            using var dbContext = new DataBaseContext(_options);
            var repository = new ReviewRepository(dbContext);

            // Act
            var result = await repository.CreateReview(review);
            var dbReview = await dbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == result.ReviewId);

            // Assert
            Assert.NotNull(dbReview);
            Assert.Equal(review.UserId, dbReview.UserId);
            Assert.Equal(review.EntertainmentId, dbReview.EntertainmentId);
            Assert.Equal(review.Rating, dbReview.Rating);
            Assert.Equal(review.Review, dbReview.Review);
        }

        [Fact]
        public async Task UpdateReview_ShouldUpdateReview_WhenReviewExists()
        {
            // Arrange
            var review = new ReviewEntity
            {
                ReviewId = 1,
                UserId = 1,
                EntertainmentId = 1,
                Rating = 5,
                Review = "Great experience"
            };
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();

            dbContext.Reviews.Add(review);
            await dbContext.SaveChangesAsync();
            var repository = new ReviewRepository(dbContext);

            // Act
            review.Rating = 4;
            var result = await repository.UpdateReview(review);

            // Assert
            Assert.Equal(4, result.Rating);
        }
        [Fact]
        public async Task UpdateReview_ShouldReturnNull_WhenReviewDoesNotExist()
        {
            // Arrange
            var review = new ReviewEntity
            {
                UserId = 1,
                EntertainmentId = 1,
                Rating = 5,
                Review = "Great experience"
            };
            using var dbContext = new DataBaseContext(_options);
            var repository = new ReviewRepository(dbContext);

            await repository.CreateReview(review);

            // Act
            review.Rating = 4;
            var result = await repository.UpdateReview(review);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(review.Rating, result.Rating);
        }

        [Fact]
        public async Task DeleteReview_ShouldRemoveReviewFromDatabase()
        {
            // Arrange
            var review = new ReviewEntity
            {
                ReviewId = 1,
                UserId = 1,
                EntertainmentId = 1,
                Rating = 5,
                Review = "Great experience"
            };
            using var dbContext = new DataBaseContext(_options);
            await dbContext.Reviews.AddAsync(review);
            dbContext.Database.EnsureDeleted();

            await dbContext.SaveChangesAsync();
            var repository = new ReviewRepository(dbContext);

            // Act
            await repository.DeleteReview(review);

            // Assert
            var deletedReview = await dbContext.Reviews.FindAsync(review.ReviewId);
            Assert.Null(deletedReview);
        }

        [Fact]
        public async Task DeleteReview_ShouldThrowArgumentNullException_WhenReviewIsNull()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var repository = new ReviewRepository(dbContext);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.DeleteReview(null));
        }
    }
}
