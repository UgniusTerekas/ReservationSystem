using DataLayer.Entities.City;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repositories.CityRepository.cs;

namespace TestLayer.Repositories
{
    public class CityRepositoryTests
    {
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;

        public CityRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "CityDatabase");
        }

        [Fact]
        public async Task GetCities_ReturnsListOfCities()
        {
            // Arrange
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            var citiesRepository = new CityRepository(dbContext);

            var cities = new List<CityEntity>
    {
        new CityEntity { CityName = "City 1", CityImage = "city1.jpg" },
        new CityEntity { CityName = "City 2", CityImage = "city2.jpg" }
    };

            await dbContext.AddRangeAsync(cities);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await citiesRepository.GetCities();

            // Assert
            Assert.Equal(cities.Count, result.Count);
            foreach (var city in cities)
            {
                Assert.Contains(city, result);
            }
        }


        [Fact]
        public async Task GetCities_WithNoCities_ReturnsEmptyList()
        {
            // Arrange
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            await dbContext.Database.EnsureDeletedAsync();
            var citiesRepository = new CityRepository(dbContext);

            // Act
            var result = await citiesRepository.GetCities();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetOneCity_WithValidCityId_ReturnsCity()
        {
            // Arrange
            var city = new CityEntity { CityName = "City 1", CityImage = "default.jpg" };
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            await dbContext.Cities.AddAsync(city);
            await dbContext.SaveChangesAsync();

            var cityRepository = new CityRepository(dbContext);

            // Act
            var result = await cityRepository.GetOneCity(city.CityId);

            // Assert
            Assert.Equal(city, result);
        }


        [Fact]
        public async Task GetOneCity_WithInvalidCityId_ReturnsNull()
        {
            // Arrange
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            var cityRepository = new CityRepository(dbContext);

            // Act
            var result = await cityRepository.GetOneCity(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateCity_WithValidCity_ReturnsCreatedCity()
        {
            // Arrange
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            var cityRepository = new CityRepository(dbContext);

            var city = new CityEntity
            {
                CityName = "City 1",
                CityImage = "sss.png" // Required CityImage property
            };

            // Act
            var result = await cityRepository.CreateCity(city);

            // Assert
            Assert.Equal(city, result);
            Assert.NotEqual(0, result.CityId);
        }

        [Fact]
        public async Task UpdateCity_WithValidCity_ReturnsUpdatedCity()
        {
            // Arrange
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            var cityRepository = new CityRepository(dbContext);

            var city = new CityEntity { CityName = "City 1", CityImage = "image.png" };
            await dbContext.Cities.AddAsync(city);
            await dbContext.SaveChangesAsync();

            // Act
            city.CityName = "City 2";
            var result = await cityRepository.UpdateCity(city);

            // Assert
            Assert.Equal(city, result);
            Assert.Equal("City 2", result.CityName);
        }

        [Fact]
        public async Task DeleteCity_WithValidCity_RemovesCityFromDbContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "DeleteCityTest")
                .Options;

            using (var context = new DataBaseContext(options))
            {
                var city = new CityEntity
                {
                    CityName = "TestCity",
                    CityImage = "testcity.jpg"
                };
                context.Cities.Add(city);
                await context.SaveChangesAsync();

                var repository = new CityRepository(context);

                // Act
                await repository.DeleteCity(city);

                // Assert
                var cities = await context.Cities.ToListAsync();
                Assert.Empty(cities);
            }
        }
    }
}
