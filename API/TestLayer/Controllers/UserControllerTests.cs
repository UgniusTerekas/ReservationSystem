using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.UserDto;
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
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserServices> _userServicesMock = new Mock<IUserServices>();

        public UserControllerTests()
        {
            _controller = new UserController(_userServicesMock.Object);
        }

        [Fact]
        public async Task GetUserInformation_ValidUserId_ReturnsOkResult()
        {
            // Arrange
            var getUserDataDto = new GetUserDataDto
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Role = "user",
                State = "active",
                RegistrationDate = DateTime.Now.AddDays(-10)
            };
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) })); // mocking HttpContext.User.FindFirstValue("UserId")
            _userServicesMock.Setup(s => s.GetUserData(userId)).ReturnsAsync(getUserDataDto);

            // Set the UserId claim in the HttpContext
           

            // Act
            var result = await _controller.GetUserInformation();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<GetUserDataDto>(okResult.Value);
            Assert.Equal(getUserDataDto.Username, actualResult.Username);
            Assert.Equal(getUserDataDto.Email, actualResult.Email);
            Assert.Equal(getUserDataDto.Role, actualResult.Role);
            Assert.Equal(getUserDataDto.State, actualResult.State);
            Assert.Equal(getUserDataDto.RegistrationDate, actualResult.RegistrationDate);
        }


        [Fact]
        public async Task GetUserInformation_InvalidUserId_ReturnsUnauthorized()
        {
            // Arrange
            var userId = 0;

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
            var result = await _controller.GetUserInformation();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task UpdateUserInformation_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var updateUserInfo = new UpdateUserInfo
            {
                UserName = "newusername",
                Email = "newemail@example.com"
            };
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) })); // mocking HttpContext.User.FindFirstValue("UserId")
            _userServicesMock.Setup(s => s.EditUserInfo(updateUserInfo, userId)).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateUserInformation(updateUserInfo);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(string.Empty, createdResult.Location);
            Assert.Equal(true, createdResult.Value);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUserInformation_InvalidData_ReturnsBadRequestResult()
        {
            // Arrange
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) })); // mocking HttpContext.User.FindFirstValue("UserId")
            var updateUserInfo = new UpdateUserInfo();
            _controller.ModelState.AddModelError("UserName", "The UserName field is required.");

            // Act
            var result = await _controller.UpdateUserInformation(updateUserInfo);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateUserInformation_UnauthorizedUser_ReturnsUnauthorizedResult()
        {
            // Arrange
            var updateUserInfo = new UpdateUserInfo
            {
                UserName = "newusername",
                Email = "newemail@example.com"
            };
            var userId = 0;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) })); // mocking HttpContext.User.FindFirstValue("UserId")

            // Act
            var result = await _controller.UpdateUserInformation(updateUserInfo);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOkResult_WhenUserIsDeleted()
        {
            // Arrange
            int userId = 123;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim("UserId", userId.ToString())
            }, "mock"));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            _userServicesMock.Setup(x => x.DeleteUser(userId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
        }

    }
}
