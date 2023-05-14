using DataLayer;
using DataLayer.Entities.User;
using DataLayer.Interfaces;
using DataLayer.Repositories.Auth;
using DataLayer.Repositories.User;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Dto.UserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Repositories
{
    public class AuthRepositoryTests
    {
        private readonly DataBaseContext _dbContext;
        private readonly IAuthRepository _authRepository;
        private readonly DbContextOptions<DataBaseContext> _options;

        public AuthRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _dbContext = new DataBaseContext(options);
            _authRepository = new AuthRepository(_dbContext);
        }

        [Fact]
        public async Task CheckUsername_WithValidUsername_ReturnsUserEntity()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var username = "testuser";
            var userEntity = new UserEntity
            {
                UserName = username,
                UserEmail = "testuser@test.com",
                Role = new Role { Name = "Admin" },
                State = new State { Name = "Active" },
                RegistrationDate = DateTime.Now,
                Password = new byte[] { 0x01, 0x02, 0x03 },
                PasswordSalt = new byte[] { 0x04, 0x05, 0x06 }
            };
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _authRepository.CheckUsername(username);

            // Assert
            Assert.Equal(username, result.UserName);
        }

        [Fact]
        public async Task CheckUsername_WithInvalidUsername_ReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "CheckUsername_WithInvalidUsername_ReturnsNull")
                .Options;
            using var context = new DataBaseContext(options);
            var authRepository = new AuthRepository(context);
            var user = new UserEntity
            {
                UserName = "testuser",
                UserEmail = "testuser@example.com",
                Password = new byte[] { 0x01, 0x02, 0x03 },
                PasswordSalt = new byte[] { 0x04, 0x05, 0x06 },
                RoleId = 1,
                StateId = 1,
                RegistrationDate = DateTime.Now
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var result = await authRepository.CheckUsername("invaliduser");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserPasswords_WithInvalidUsername_ReturnsNull()
        {
            // Arrange
            var username = "invalidusername";

            // Act
            var result = await _authRepository.GetUserPasswords(username);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserPasswords_WithValidUsername_ReturnsUserPasswordsDto()
        {
            // Arrange
            var username = "testuser";
            var userEntity = new UserEntity
            {
                UserName = username,
                UserEmail = "testuser@test.com",
                Role = new Role { Name = "Admin" },
                State = new State { Name = "Active" },
                RegistrationDate = DateTime.Now,
                Password = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 }
            };
            await _dbContext.Users.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _authRepository.GetUserPasswords(username);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserPasswordsDto>(result);
            Assert.Equal(userEntity.Password, result.PasswordHash);
            Assert.Equal(userEntity.PasswordSalt, result.PasswordSalt);
        }

        [Fact]
        public async Task CreateUser_WithValidUser_ReturnsCreatedUser()
        {
            // Arrange
            var userEntity = new UserEntity
            {
                UserName = "testuser",
                UserEmail = "testuser@test.com",
                Password = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 },
                Role = new Role { Name = "Admin" },
                State = new State { Name = "Active" },
                RegistrationDate = DateTime.Now
            };

            // Act
            var result = await _authRepository.CreateUser(userEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userEntity.UserName, result.UserName);
            Assert.Equal(userEntity.UserEmail, result.UserEmail);
            Assert.Equal(userEntity.Password, result.Password);
            Assert.Equal(userEntity.PasswordSalt, result.PasswordSalt);
            Assert.Equal(userEntity.Role.Name, result.Role.Name);
            Assert.Equal(userEntity.State.Name, result.State.Name);
            Assert.Equal(userEntity.RegistrationDate, result.RegistrationDate);
        }


    }
}
