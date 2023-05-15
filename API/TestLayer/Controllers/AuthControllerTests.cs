using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Contracts.Auth;
using ModelLayer.Dto.UserDto;
using Moq;
using ServiceLayer.Interfaces;

namespace TestLayer.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Register_Returns_Successful_Response()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                UserRegisterDto = new UserRegisterDto
                {
                    UserName = "testuser",
                    Email = "testuser@example.com",
                    Password = "testpassword"
                }
            };
            var mockAuthService = new Mock<IAuthServices>();
            mockAuthService.Setup(x => x.Register(registerRequest))
                .ReturnsAsync(new RegisterResponse { IsSuccess = true });
            var controller = new AuthController(mockAuthService.Object);

            // Act
            var result = await controller.Register(registerRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<RegisterResponse>(okResult.Value);
            Assert.True(response.IsSuccess);
        }

        [Fact]
        public async Task Register_Returns_BadRequest_When_RegisterContract_Is_Null()
        {
            // Arrange
            RegisterRequest registerContract = null;
            var mockAuthService = new Mock<IAuthServices>();
            var controller = new AuthController(mockAuthService.Object);

            // Act
            var result = await controller.Register(registerContract);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Register_Returns_BadRequest_When_ModelState_Is_Invalid()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                UserRegisterDto = new UserRegisterDto
                {
                    UserName = "",
                    Email = "testuser@example.com",
                    Password = "testpassword"
                }
            };
            var mockAuthService = new Mock<IAuthServices>();
            var controller = new AuthController(mockAuthService.Object);
            controller.ModelState.AddModelError("UserName", "The UserName field is required.");

            // Act
            var result = await controller.Register(registerRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                UserLoginRequest = new UserLoginDto
                {
                    Username = "testuser",
                    Password = "testpassword"
                }
            };
            var mockAuthService = new Mock<IAuthServices>();
            mockAuthService.Setup(x => x.Login(loginRequest)).ReturnsAsync(new LoginResponse { TokenJWT = "fake-token" });
            var controller = new AuthController(mockAuthService.Object);

            // Act
            var result = await controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.Equal("fake-token", response.TokenJWT);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsEmptyToken()
        {
            // Arrange
            var loginRequest = new LoginRequest
            {
                UserLoginRequest = new UserLoginDto
                {
                    Username = "testuser",
                    Password = "wrongpassword"
                }
            };
            var mockAuthService = new Mock<IAuthServices>();
            mockAuthService.Setup(x => x.Login(loginRequest)).ReturnsAsync(new LoginResponse { TokenJWT = "" });
            var controller = new AuthController(mockAuthService.Object);

            // Act
            var result = await controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.Equal("", response.TokenJWT);
        }
    }

}
