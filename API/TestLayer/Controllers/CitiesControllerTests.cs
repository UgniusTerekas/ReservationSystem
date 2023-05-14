using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Contracts.City;
using ModelLayer.Dto.City;
using Moq;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Controllers
{
    public class CitiesControllerTests
    {
        private readonly Mock<ICitiesServices> _mockCitiesService;
        private readonly CityController _controller;

        public CitiesControllerTests()
        {
            _mockCitiesService = new Mock<ICitiesServices>();
            _controller = new CityController(_mockCitiesService.Object);
        }

        [Fact]
        public async Task GetCity_ValidCityId_ReturnsCityDto()
        {
            // Arrange
            var expectedCity = new CityDto
            {
                CityId = 1,
                CityName = "Test City",
                CityImage = "test.png"
            };
            _mockCitiesService.Setup(x => x.GetCity(expectedCity.CityId)).ReturnsAsync(expectedCity);

            // Act
            var result = await _controller.GetCity(expectedCity.CityId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var actualCity = Assert.IsType<CityDto>(okResult.Value);
            Assert.Equal(expectedCity.CityId, actualCity.CityId);
            Assert.Equal(expectedCity.CityName, actualCity.CityName);
            Assert.Equal(expectedCity.CityImage, actualCity.CityImage);
        }

        [Fact]
        public async Task GetCities_ShouldReturnOkObjectResultWithCities_WhenCitiesExist()
        {
            // Arrange
            var cities = new List<CityDto>
        {
            new CityDto
            {
                CityId = 1,
                CityName = "New York",
                CityImage = "new-york.jpg"
            },
            new CityDto
            {
                CityId = 2,
                CityName = "Los Angeles",
                CityImage = "los-angeles.jpg"
            }
        };

            _mockCitiesService.Setup(x => x.GetCities()).ReturnsAsync(cities);

            // Act
            var result = await _controller.GetCities();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<CityDto>>(okObjectResult.Value);
            Assert.Equal(cities.Count, resultValue.Count);
            Assert.Equal(cities[0].CityName, resultValue[0].CityName);
            Assert.Equal(cities[0].CityImage, resultValue[0].CityImage);
            Assert.Equal(cities[1].CityName, resultValue[1].CityName);
            Assert.Equal(cities[1].CityImage, resultValue[1].CityImage);
        }

        [Fact]
        public async Task GetCities_ShouldReturnOkObjectResultWithEmptyList_WhenNoCitiesExist()
        {
            // Arrange
            var cities = new List<CityDto>();

            _mockCitiesService.Setup(x => x.GetCities()).ReturnsAsync(cities);

            // Act
            var result = await _controller.GetCities();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<CityDto>>(okObjectResult.Value);
            Assert.Empty(resultValue);
        }

        [Fact]
        public async Task GetCity_InvalidCityId_ReturnsBadRequest()
        {
            // Arrange
            var cityId = 0;
            _mockCitiesService.Setup(x => x.GetCity(cityId)).ReturnsAsync((CityDto)null);


            // Act
            var result = await _controller.GetCity(cityId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        /*[Fact]
        public async Task CreateCity_WithValidData_ReturnsCreated()
        {
            // Arrange
            var name = "New City";
            var file = new Mock<IFormFile>();
            var cityRequest = new CityRequest { CityName = name, CityImage = file.Object };
            var cityDto = new CityDto { CityId = 1, CityName = name, CityImage = "image.png" };

            _mockCitiesService.Setup(x => x.CreateCity(cityRequest)).ReturnsAsync(cityDto);

            // Act
            var result = await _controller.CreateCity(name, file.Object);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<CityDto>(createdResult.Value);
            Assert.Equal(cityDto.CityId, model.CityId);
            Assert.Equal(cityDto.CityName, model.CityName);
            Assert.Equal(cityDto.CityImage, model.CityImage);
        }*/

        [Fact]
        public async Task CreateCity_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var name = "New City";
            IFormFile file = null;
            var cityRequest = new CityRequest { CityName = name, CityImage = file };
            CityDto cityDto = null;

            _mockCitiesService.Setup(x => x.CreateCity(cityRequest)).ReturnsAsync(cityDto);

            // Act
            var result = await _controller.CreateCity(name, file);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        /*[Fact]
        public async Task UpdateCity_WithValidData_ReturnsNoContent()
        {
            // Arrange
            int cityId = 1;
            string cityName = "New York";
            IFormFile cityImage = new FormFile(Stream.Null, 0, 0, "cityImage", "city.png");

            UpdateCityRequest updateCityRequest = new UpdateCityRequest
            {
                CityId = cityId,
                CityName = cityName,
                CityImage = cityImage
            };

            _mockCitiesService.Setup(x => x.UpdateCity(updateCityRequest, cityImage)).ReturnsAsync(new CityDto());

            // Act
            IActionResult result = await _controller.UpdateCity(cityId, cityName, cityImage);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }*/

        [Fact]
        public async Task UpdateCity_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            int cityId = 1;
            string cityName = "New York";
            IFormFile cityImage = null;

            UpdateCityRequest updateCityRequest = new UpdateCityRequest
            {
                CityId = cityId,
                CityName = cityName,
                CityImage = cityImage
            };

            _mockCitiesService.Setup(x => x.UpdateCity(updateCityRequest, cityImage)).ReturnsAsync((CityDto)null);

            // Act
            IActionResult result = await _controller.UpdateCity(cityId, cityName, cityImage);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCity_ValidCityId_Returns204()
        {
            // Arrange
            int cityId = 1;
            _mockCitiesService.Setup(x => x.DeleteCity(cityId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCity(cityId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.True((bool)okResult.Value);
        }

        [Fact]
        public async Task DeleteCity_InvalidCityId_ReturnsBadRequest()
        {
            // Arrange
            int cityId = 1;
            _mockCitiesService.Setup(x => x.DeleteCity(cityId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCity(cityId);

            // Assert
            Assert.IsType<BadRequestResult>(result);
            var badRequestResult = result as BadRequestResult;
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }
    }
}
