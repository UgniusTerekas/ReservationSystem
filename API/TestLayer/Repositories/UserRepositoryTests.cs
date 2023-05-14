using DataLayer.Entities.User;
using DataLayer.Repositories.User;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Repositories
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<DataBaseContext> _options;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;
        }

        [Fact]
        public async Task GetUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;
            using var dbContext = new DataBaseContext(_options);
            var repository = new UserRepository(dbContext);

            // Act
            var result = await repository.GetUser(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task EditUser_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var user = new UserEntity
            {
                UserId = 1,
                UserName = "John",
                UserEmail = "john@example.com",
                Password = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 },
                RegistrationDate = DateTime.Now,
                RoleId = 1,
                StateId = 1
            };
            using var dbContext = new DataBaseContext(_options);
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var repository = new UserRepository(dbContext);

            // Act
            user.UserName = "Jane";
            var result = await repository.EditUser(user);

            // Assert
            Assert.True(result);
            Assert.Equal("Jane", dbContext.Users.Single(u => u.UserId == user.UserId).UserName);
        }

        [Fact]
        public async Task EditUser_ShouldThrowArgumentNullException_WhenEditUserIsNull()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var repository = new UserRepository(dbContext);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.EditUser(null));
        }

        [Fact]
        public async Task DeleteUser_ShouldRemoveUser_WhenUserExists()
        {
            // Arrange
            var user = new UserEntity
            {
                UserId = 1,
                UserName = "John",
                UserEmail = "john@example.com",
                Password = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 },
                RegistrationDate = DateTime.Now,
                RoleId = 1,
                StateId = 1
            };
            using var dbContext = new DataBaseContext(_options);
            dbContext.Database.EnsureDeleted();
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
            var repository = new UserRepository(dbContext);

            // Act
            var result = await repository.DeleteUser(user);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(user, dbContext.Users);
        }
    }
}
