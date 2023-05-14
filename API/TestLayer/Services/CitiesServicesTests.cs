using DataLayer.Entities.City;
using DataLayer.Interfaces;
using ModelLayer.Dto.City;
using Moq;
using ServiceLayer.CityService;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Services
{
    public class CitiesServicesTests
    {
        private readonly Mock<ICityRepository> _cityRepositoryMock;
        private readonly CitiesServices _cityServices;

        public CitiesServicesTests()
        {
            _cityRepositoryMock = new Mock<ICityRepository>();
            _cityServices = new CitiesServices(_cityRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCity_WithValidCityId_ReturnsCityDto()
        {
            // Arrange
            int cityId = 1;
            var city = new CityEntity { CityId = cityId, CityName = "Test City", CityImage = "testimage.jpg" };
            _cityRepositoryMock.Setup(r => r.GetOneCity(cityId)).ReturnsAsync(city);

            // Act
            var result = await _cityServices.GetCity(cityId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CityDto>(result);
            Assert.Equal(cityId, result.CityId);
            Assert.Equal(city.CityName, result.CityName);
            Assert.Equal(city.CityImage, result.CityImage);
        }

        [Fact]
        public async Task GetCity_WithInvalidCityId_ReturnsNull()
        {
            // Arrange
            int cityId = 0;

            // Act
            var result = await _cityServices.GetCity(cityId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCity_WithNonExistingCityId_ReturnsNull()
        {
            // Arrange
            int cityId = 1;
            _cityRepositoryMock.Setup(r => r.GetOneCity(cityId)).ReturnsAsync((CityEntity)null);

            // Act
            var result = await _cityServices.GetCity(cityId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteCity_WithExistingCity_ReturnsTrue()
        {
            // Arrange
            int cityId = 1;
            var city = new CityEntity { CityId = cityId, CityName = "Test City", CityImage = "test.jpg" };
            _cityRepositoryMock.Setup(r => r.GetOneCity(cityId)).ReturnsAsync(city);

            // Act
            bool result = await _cityServices.DeleteCity(cityId);

            // Assert
            Assert.True(result);
            _cityRepositoryMock.Verify(r => r.DeleteCity(city), Times.Once);
        }

        [Fact]
        public async Task DeleteCity_WithNonExistingCity_ReturnsFalse()
        {
            // Arrange
            int cityId = 1;
            _cityRepositoryMock.Setup(r => r.GetOneCity(cityId)).ReturnsAsync((CityEntity)null);

            // Act
            bool result = await _cityServices.DeleteCity(cityId);

            // Assert
            Assert.False(result);
            _cityRepositoryMock.Verify(r => r.DeleteCity(It.IsAny<CityEntity>()), Times.Never);
        }
    }
}
