using DataLayer.Entities.User;
using DataLayer.Interfaces;
using ModelLayer.Dto.UserDto;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Review;
using ServiceLayer.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Services
{
    public class UserServicesTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserServices _userServices;

        public UserServicesTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authRepositoryMock = new Mock<IAuthRepository>();
            _userServices = new UserServices(_userRepositoryMock.Object, _authRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUserData_WithValidUserId_ReturnsGetUserDataDto()
        {
            // Arrange
            var userId = 1;

            _userRepositoryMock.Setup(r => r.GetUser(userId)).ReturnsAsync(new UserEntity
            {
                UserId = userId,
                UserName = "testuser",
                UserEmail = "testuser@test.com",
                Role = new Role { Name = "Admin" },
                State = new State { Name = "Active" },
                RegistrationDate = DateTime.Now
            });

            // Act
            var result = await _userServices.GetUserData(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GetUserDataDto>(result);
        }

        [Fact]
        public async Task GetUserData_WithInvalidUserId_ReturnsNull()
        {
            // Arrange
            var userId = -1;
            _userRepositoryMock.Setup(repo => repo.GetUser(userId)).ReturnsAsync((UserEntity)null);

            // Act
            var result = await _userServices.GetUserData(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task EditUserInfo_WithValidData_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var userRepositoryMock = new Mock<IUserRepository>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            var userEntity = new UserEntity
            {
                UserId = id,
                UserName = "testuser",
                UserEmail = "testuser@test.com"
            };
            userRepositoryMock.Setup(m => m.GetUser(id))
                .ReturnsAsync(userEntity);
            authRepositoryMock.Setup(m => m.CheckUsername(It.IsAny<string>()))
                .ReturnsAsync((UserEntity)null);
            userRepositoryMock.Setup(m => m.EditUser(userEntity))
                .ReturnsAsync(true);
            var userServices = new UserServices(userRepositoryMock.Object, authRepositoryMock.Object);
            var updateUser = new UpdateUserInfo
            {
                UserName = "newusername",
                Email = "newemail@test.com"
            };

            // Act
            var result = await userServices.EditUserInfo(updateUser, id);

            // Assert
            Assert.True(result);
            Assert.Equal(updateUser.UserName, userEntity.UserName);
            Assert.Equal(updateUser.Email, userEntity.UserEmail);
        }

        [Fact]
        public async Task EditUserInfo_WithExistingUsername_ReturnsFalse()
        {
            // Arrange
            var id = 1;
            var userRepositoryMock = new Mock<IUserRepository>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            var userEntity = new UserEntity
            {
                UserId = id,
                UserName = "testuser",
                UserEmail = "testuser@test.com"
            };
            userRepositoryMock.Setup(m => m.GetUser(id))
                .ReturnsAsync(userEntity);
            authRepositoryMock.Setup(m => m.CheckUsername(It.IsAny<string>()))
                .ReturnsAsync(new UserEntity());
            var userServices = new UserServices(userRepositoryMock.Object, authRepositoryMock.Object);
            var updateUser = new UpdateUserInfo
            {
                UserName = "existingusername",
                Email = "newemail@test.com"
            };

            // Act
            var result = await userServices.EditUserInfo(updateUser, id);

            // Assert
            Assert.False(result);
            Assert.Equal("testuser", userEntity.UserName);
            Assert.Equal("testuser@test.com", userEntity.UserEmail);
        }

        [Fact]
        public async Task EditUserInfo_WithInvalidData_ReturnsFalse()
        {
            // Arrange
            var id = -1;
            var userRepositoryMock = new Mock<IUserRepository>();
            var authRepositoryMock = new Mock<IAuthRepository>();
            var userEntity = new UserEntity
            {
                UserId = id,
                UserName = "testuser",
                UserEmail = "testuser@test.com"
            };
            userRepositoryMock.Setup(m => m.GetUser(id))
                .ReturnsAsync(userEntity);
            var userServices = new UserServices(userRepositoryMock.Object, authRepositoryMock.Object);
            var updateUser = new UpdateUserInfo();

            // Act
            var result = await userServices.EditUserInfo(updateUser, id);

            // Assert
            Assert.False(result);
            Assert.Equal("testuser", userEntity.UserName);
            Assert.Equal("testuser@test.com", userEntity.UserEmail);
        }

        [Fact]
        public async Task DeleteUser_WithValidUserId_ReturnsTrue()
        {
            // Arrange
            int userId = 1;
            var userEntity = new UserEntity { UserId = userId };
            _userRepositoryMock.Setup(repo => repo.GetUser(userId))
                .ReturnsAsync(userEntity);
            _userRepositoryMock.Setup(repo => repo.DeleteUser(userEntity))
                .ReturnsAsync(true);

            // Act
            bool result = await _userServices.DeleteUser(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUser_WithInvalidUserId_ReturnsFalse()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetUser(userId))
                .ReturnsAsync((UserEntity)null);

            // Act
            bool result = await _userServices.DeleteUser(userId);

            // Assert
            Assert.False(result);
        }
    }
}
