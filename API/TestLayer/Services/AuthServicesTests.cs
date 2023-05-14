using DataLayer.Entities.User;
using DataLayer.Interfaces;
using DataLayer.Repositories.Auth;
using ModelLayer.Contracts.Auth;
using ModelLayer.Dto.UserDto;
using Moq;
using ServiceLayer.AuthServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq.Language.Flow;

namespace TestLayer.Services
{
    public class AuthServicesTests
    {
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthServices _authServices;

        public AuthServicesTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _authRepositoryMock = new Mock<IAuthRepository>();
            _authServices = new AuthServices(_authRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Register_ValidData_ReturnsRegisterResponse()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                UserRegisterDto = new UserRegisterDto
                {
                    UserName = "testuser",
                    Email = "testuser@test.com",
                    Password = "testpassword",
                },
            };

            _authRepositoryMock.Setup(repo => repo.CheckUsername(registerRequest.UserRegisterDto.UserName)).ReturnsAsync((UserEntity)null);
            _authRepositoryMock.Setup(repo => repo.CreateUser(It.IsAny<UserEntity>())).ReturnsAsync(new UserEntity());


            // Act
            var result = await _authServices.Register(registerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Register_UsernameAlreadyExists_ReturnsNull()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                UserRegisterDto = new UserRegisterDto
                {
                    UserName = "existinguser",
                    Email = "newuser@test.com",
                    Password = "newpassword",
                },
            };
            var existingUser = new UserEntity { UserName = "existinguser" };
            _authRepositoryMock.Setup(repo => repo.CheckUsername(registerRequest.UserRegisterDto.UserName)).ReturnsAsync(existingUser);

            // Act
            var result = await _authServices.Register(registerRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_Returns_Null_If_User_Not_Found()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                UserLoginRequest = new UserLoginDto
                {
                    Username = "nonexistentuser",
                    Password = "password123",
                }
            };
            _authRepositoryMock.Setup(repo => repo.CheckUsername(loginRequest.UserLoginRequest.Username))
                               .ReturnsAsync((UserEntity)null);

            // Act
            var result = await _authServices.Login(loginRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_Returns_Null_If_Password_Validation_Fails()
        {
            // Arrange
            var user = new UserEntity
            {
                UserId = 1,
                UserName = "existinguser",
                PasswordSalt = new byte[] { 1, 2, 3 },
                Password = new byte[] { 4, 5, 6 },
            };
            var loginRequest = new LoginRequest
            {
                UserLoginRequest = new UserLoginDto
                {
                    Username = user.UserName,
                    Password = "wrongpassword",
                }
            };
            _authRepositoryMock.Setup(repo => repo.CheckUsername(loginRequest.UserLoginRequest.Username))
                               .ReturnsAsync(user);

            // Act
            var result = await _authServices.Login(loginRequest);

            // Assert
            Assert.Null(result);
        }
    }
}
