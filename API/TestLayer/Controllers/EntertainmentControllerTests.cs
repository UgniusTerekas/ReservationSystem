using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Gallery;
using Moq;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Controllers
{
    public class EntertainmentControllerTests
    {
        private readonly Mock<IEntertainmentServices> _mockEntertainmentServices;
        private readonly EntertainmentController _controller;
        public EntertainmentControllerTests()
        {
            _mockEntertainmentServices = new Mock<IEntertainmentServices>();
            _controller = new EntertainmentController(_mockEntertainmentServices.Object);
        }

        [Fact]
        public async Task GetCityEntertainmentList_ReturnsOkObjectResult_WhenResultIsNotNull()
        {
            // Arrange
            var cityId = 1;
            var entertainments = new List<EntertainmentCardDto>
        {
            new EntertainmentCardDto { Id = 1, Name = "Entertainment 1", Rating = 4.5, Price = 10.0 },
            new EntertainmentCardDto { Id = 2, Name = "Entertainment 2", Rating = 3.5, Price = 20.0 }
        };
            _mockEntertainmentServices.Setup(x => x.GetCityEntertainments(cityId)).ReturnsAsync(entertainments);

            // Act
            var result = await _controller.GetCityEntertainmentList(cityId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<EntertainmentCardDto>>(okResult.Value);
            Assert.Equal(2, model.Count);
            Assert.Equal(entertainments, model);
        }

        [Fact]
        public async Task GetEntertainmentList_ShouldReturnOkObjectResultWithEntertainmentList()
        {
            // Arrange
            var entertainments = new List<EntertainmentCardDto>
        {
            new EntertainmentCardDto
            {
                Id = 1,
                Image = new GalleryDto
                {
                    ImageId = 1,
                    ImageName = "image1.jpg",
                    ImageLocation = "https://example.com/images/image1.jpg",
                    EntertainmentId = 1
                },
                Name = "Entertainment 1",
                Rating = 4.5,
                Price = 10.0
            },
            new EntertainmentCardDto
            {
                Id = 2,
                Image = new GalleryDto
                {
                    ImageId = 2,
                    ImageName = "image2.jpg",
                    ImageLocation = "https://example.com/images/image2.jpg",
                    EntertainmentId = 2
                },
                Name = "Entertainment 2",
                Rating = 3.5,
                Price = 5.0
            }
        };

            _mockEntertainmentServices.Setup(x => x.GetEntertainments()).ReturnsAsync(entertainments);

            // Act
            var result = await _controller.GetEntertainmentList();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<EntertainmentCardDto>>(okObjectResult.Value);
            Assert.Equal(entertainments.Count, resultValue.Count);

            for (int i = 0; i < entertainments.Count; i++)
            {
                var expected = entertainments[i];
                var actual = resultValue[i];

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.Name, actual.Name);
                Assert.Equal(expected.Rating, actual.Rating);
                Assert.Equal(expected.Price, actual.Price);

                if (expected.Image == null)
                {
                    Assert.Null(actual.Image);
                }
                else
                {
                    Assert.NotNull(actual.Image);
                    Assert.Equal(expected.Image.ImageId, actual.Image.ImageId);
                    Assert.Equal(expected.Image.ImageName, actual.Image.ImageName);
                    Assert.Equal(expected.Image.ImageLocation, actual.Image.ImageLocation);
                    Assert.Equal(expected.Image.EntertainmentId, actual.Image.EntertainmentId);
                }
            }
        }

        [Fact]
        public async Task GetEntertainmentList_ShouldReturnOkObjectResultWithEmptyList_WhenNoEntertainmentsExist()
        {
            // Arrange
            var entertainments = new List<EntertainmentCardDto>();

            _mockEntertainmentServices.Setup(x => x.GetEntertainments()).ReturnsAsync(entertainments);

            // Act
            var result = await _controller.GetEntertainmentList();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<EntertainmentCardDto>>(okObjectResult.Value);
            Assert.Empty(resultValue);
        }


        [Fact]
        public async Task GetCityEntertainmentList_ReturnsBadRequest_WhenResultIsNull()
        {
            // Arrange
            var cityId = 1;
            _mockEntertainmentServices.Setup(x => x.GetCityEntertainments(cityId)).ReturnsAsync((List<EntertainmentCardDto>)null);

            // Act
            var result = await _controller.GetCityEntertainmentList(cityId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetCategoryEntertainmentList_ValidRequest_ReturnsList()
        {
            //Arrange
            int categoryId = 1;
            var mockEntertainmentService = new Mock<IEntertainmentServices>();
            var expectedList = new List<EntertainmentCardDto>()
    {
        new EntertainmentCardDto() { Id = 1, Image = new GalleryDto(), Name = "Entertainment1", Rating = 4.5, Price = 100 },
        new EntertainmentCardDto() { Id = 2, Image = new GalleryDto(), Name = "Entertainment2", Rating = 3.2, Price = 50 }
    };
            mockEntertainmentService.Setup(x => x.GetCategoryEntertainments(categoryId)).ReturnsAsync(expectedList);
            var controller = new EntertainmentController(mockEntertainmentService.Object);

            //Act
            var result = await controller.GetCategoryEntertainmentList(categoryId) as OkObjectResult;
            var resultList = result.Value as List<EntertainmentCardDto>;

            //Assert
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedList.Count, resultList.Count);
            for (int i = 0; i < resultList.Count; i++)
            {
                Assert.Equal(expectedList[i].Id, resultList[i].Id);
                Assert.Equal(expectedList[i].Name, resultList[i].Name);
                Assert.Equal(expectedList[i].Rating, resultList[i].Rating);
                Assert.Equal(expectedList[i].Price, resultList[i].Price);
            }
        }

        [Fact]
        public async Task GetCategoryEntertainmentList_CategoryDoesNotExist_ReturnsBadRequest()
        {
            //Arrange
            int categoryId = 1;
            var mockEntertainmentService = new Mock<IEntertainmentServices>();
            mockEntertainmentService.Setup(x => x.GetCategoryEntertainments(categoryId)).ReturnsAsync((List<EntertainmentCardDto>)null);
            var controller = new EntertainmentController(mockEntertainmentService.Object);

            //Act
            var result = await controller.GetCategoryEntertainmentList(categoryId) as BadRequestResult;

            //Assert
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GetEntertainmentDetails_ReturnsOkResult_WhenIdIsValid()
        {
            // Arrange
            int validId = 1;
            var entertainmentDto = new EntertainmentDto { Name = "Entertainment 1" };
            _mockEntertainmentServices.Setup(x => x.GetEntertainmentDetails(validId)).ReturnsAsync(entertainmentDto);

            // Act
            var result = await _controller.GetEntertainmentDetails(validId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedEntertainmentDto = Assert.IsType<EntertainmentDto>(okResult.Value);
            Assert.Equal(entertainmentDto.Name, returnedEntertainmentDto.Name);
        }

        [Fact]
        public async Task GetEntertainmentDetails_ReturnsBadRequestResult_WhenIdIsInvalid()
        {
            // Arrange
            int invalidId = 0;
            _mockEntertainmentServices.Setup(x => x.GetEntertainmentDetails(invalidId)).ReturnsAsync((EntertainmentDto)null);

            // Act
            var result = await _controller.GetEntertainmentDetails(invalidId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CreateEntertainment_ValidEntertainment_ReturnsCreated()
        {
            // Arrange
            var createEntertainmentDto = new CreateEntertainmentDto
            {
                Name = "Test name",
                Description = "Test description",
                Price = 10.5,
                PhoneNumber = "1234567890",
                Address = "Test address",
                Email = "test@test.com",
                CitiesIds = new List<int> { 1, 2 },
                CategoriesIds = new List<int> { 3, 4 }
            };
            var mockEntertainmentServices = new Mock<IEntertainmentServices>();
            mockEntertainmentServices.Setup(s => s.CreateEntertainment(It.IsAny<CreateEntertainmentDto>(), It.IsAny<int>())).ReturnsAsync(1);
            var controller = new EntertainmentController(mockEntertainmentServices.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
        new Claim("UserId", "1")
    }));

            // Act
            var result = await controller.CreateEntertainment(createEntertainmentDto);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(1, createdResult.Value);
        }

        [Fact]
        public async Task CreateEntertainment_InvalidEntertainment_ReturnsBadRequest()
        {
            // Arrange
            var createEntertainmentDto = new CreateEntertainmentDto
            {
                Name = null,
                Description = "Test description",
                Price = 10.5,
                PhoneNumber = "1234567890",
                Address = "Test address",
                Email = "test@test.com",
                CitiesIds = new List<int> { 1, 2 },
                CategoriesIds = new List<int> { 3, 4 }
            };
            var mockEntertainmentServices = new Mock<IEntertainmentServices>();
            mockEntertainmentServices.Setup(s => s.CreateEntertainment(It.IsAny<CreateEntertainmentDto>(), It.IsAny<int>())).ReturnsAsync(-1);
            var controller = new EntertainmentController(mockEntertainmentServices.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
        new Claim("UserId", "1")
    }));

            // Act
            var result = await controller.CreateEntertainment(createEntertainmentDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task CreateEntertainment_MissingUserId_ReturnsUnauthorized()
        {
            // Arrange
            var createEntertainmentDto = new CreateEntertainmentDto
            {
                Name = "Test name",
                Description = "Test description",
                Price = 10.5,
                PhoneNumber = "1234567890",
                Address = "Test address",
                Email = "test@test.com",
                CitiesIds = new List<int> { 1, 2 },
                CategoriesIds = new List<int> { 3, 4 }
            };
            var mockEntertainmentServices = new Mock<IEntertainmentServices>();
            var controller = new EntertainmentController(mockEntertainmentServices.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
        new Claim(ClaimTypes.Name, "testuser"),
        new Claim(ClaimTypes.Email, "test@test.com"),
        new Claim(ClaimTypes.Role, "admin"),
                // missing UserId claim
            }));

            // Act
            var result = await controller.CreateEntertainment(createEntertainmentDto);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }

        [Fact]
        public async Task UpdateEntertainment_ValidInput_ReturnsNoContent()
        {
            // Arrange
            var updateEntertainmentDto = new UpdateEntertainmentDto { /* valid input */ };
            _mockEntertainmentServices
                .Setup(x => x.UpdateEntertainment(updateEntertainmentDto))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateEntertainment(updateEntertainmentDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateEntertainment_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var updateEntertainmentDto = new UpdateEntertainmentDto { /* invalid input */ };
            _mockEntertainmentServices
                .Setup(x => x.UpdateEntertainment(updateEntertainmentDto))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateEntertainment(updateEntertainmentDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateEntertainment_NullInput_ReturnsBadRequest()
        {
            // Arrange
            UpdateEntertainmentDto updateEntertainmentDto = null;

            // Act
            var result = await _controller.UpdateEntertainment(updateEntertainmentDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteEntertainment_ValidInput_ReturnsNoContent()
        {
            // Arrange
            var id = 1;
            _mockEntertainmentServices
                .Setup(x => x.DeleteEntertainment(id))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteEntertainment(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteEntertainment_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            var id = 0;
            _mockEntertainmentServices
                .Setup(x => x.DeleteEntertainment(id))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteEntertainment(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetEntertainmentForEdit_ValidUser_ReturnsOk()
        {
            // Arrange
            var userId = 1;
            var expectedResult = new List<GetEntertainmentForEditing>
        {
            new GetEntertainmentForEditing { Id = 1, Name = "Test Entertainment" }
        };
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("UserId", userId.ToString())
                }))
                }
            };
            _mockEntertainmentServices
                .Setup(x => x.GetEntertainmentForEditing(userId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.GetEntertainmentForEdit();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<List<GetEntertainmentForEditing>>(okResult.Value);
            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult[0].Id, actualResult[0].Id);
            Assert.Equal(expectedResult[0].Name, actualResult[0].Name);
        }

        [Fact]
        public async Task GetEntertainmentForEdit_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var userId = 0;
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("UserId", userId.ToString())
                }))
                }
            };

            // Act
            var result = await _controller.GetEntertainmentForEdit();

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
