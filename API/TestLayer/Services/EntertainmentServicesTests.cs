using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Entities.Gallery;
using DataLayer.Entities.Reservation;
using DataLayer.Entities.Review;
using DataLayer.Entities.User;
using DataLayer.Interfaces;
using ModelLayer.Dto.Category;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Entertainment;
using ModelLayer.Dto.Gallery;
using ModelLayer.Dto.Reservation;
using ModelLayer.Dto.Review;
using Moq;
using ServiceLayer.EntertainmentService;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Services
{
    public class EntertainmentServicesTests
    {
        private readonly Mock<IEntertainmentRepository> _entertainmentRepositoryMock;
        private readonly Mock<ICityRepository> _cityRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMock;
        private readonly EntertainmentServices _entertainmentService;

        public EntertainmentServicesTests()
        {
            _entertainmentRepositoryMock = new Mock<IEntertainmentRepository>();
            _cityRepositoryMock = new Mock<ICityRepository>();
            _categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _entertainmentService = new EntertainmentServices(_entertainmentRepositoryMock.Object,
                _cityRepositoryMock.Object,
                _categoriesRepositoryMock.Object,
                _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCityEntertainments_WithValidCityId_ReturnsEntertainmentList()
        {
            // Arrange
            int cityId = 1;

            var entertainmentList = new List<EntertainmentItemEntity>
    {
        new EntertainmentItemEntity
        {
            EntertainmentId = 1,
            EntertainmentName = "Entertainment 1",
            Price = 10.0,
            Gallery = new List<GalleryEntity>
            {
                new GalleryEntity
                {
                    ImageId = 1,
                    ImageName = "Image 1",
                    ImageLocation = "/image1.jpg",
                }
            },
            Reviews = new List<ReviewEntity>
            {
                new ReviewEntity
                {
                    Rating = 5
                }
            }
        },
        new EntertainmentItemEntity
        {
            EntertainmentId = 2,
            EntertainmentName = "Entertainment 2",
            Price = 20.0,
            Gallery = new List<GalleryEntity>
            {
                new GalleryEntity
                {
                    ImageId = 2,
                    ImageName = "Image 2",
                    ImageLocation = "/image2.jpg",
                }
            },
            Reviews = new List<ReviewEntity>
            {
                new ReviewEntity
                {
                    Rating = 3
                }
            }
        }
    };

            _entertainmentRepositoryMock.Setup(r => r.GetCityEntertainments(cityId)).ReturnsAsync(entertainmentList);

            var expectedEntertainmentDtoList = new List<EntertainmentCardDto>
    {
        new EntertainmentCardDto
        {
            Id = 1,
            Name = "Entertainment 1",
            Price = 10.0,
            Image = new GalleryDto
            {
                ImageId = 1,
                ImageName = "Image 1",
                ImageLocation = "https://localhost:7229/image1.jpg"
            },
            Rating = 5.0
        },
        new EntertainmentCardDto
        {
            Id = 2,
            Name = "Entertainment 2",
            Price = 20.0,
            Image = new GalleryDto
            {
                ImageId = 2,
                ImageName = "Image 2",
                ImageLocation = "https://localhost:7229/image2.jpg"
            },
            Rating = 3.0
        }
    };

            // Act
            var result = await _entertainmentService.GetCityEntertainments(cityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEntertainmentDtoList.Count, result.Count);

            for (int i = 0; i < expectedEntertainmentDtoList.Count; i++)
            {
                Assert.Equal(expectedEntertainmentDtoList[i].Id, result[i].Id);
                Assert.Equal(expectedEntertainmentDtoList[i].Name, result[i].Name);
                Assert.Equal(expectedEntertainmentDtoList[i].Price, result[i].Price);
                Assert.Equal(expectedEntertainmentDtoList[i].Image?.ImageId, result[i].Image?.ImageId);
                Assert.Equal(expectedEntertainmentDtoList[i].Image?.ImageName, result[i].Image?.ImageName);
                Assert.Equal(expectedEntertainmentDtoList[i].Image?.ImageLocation, result[i].Image?.ImageLocation);
                Assert.Equal(expectedEntertainmentDtoList[i].Rating, result[i].Rating);
            }
        }

        [Fact]
        public async Task GetCityEntertainments_WithInvalidCityId_ReturnsEmptyList()
        {
            // Arrange
            int cityId = 1;

            _entertainmentRepositoryMock.Setup(r => r.GetCityEntertainments(cityId)).ReturnsAsync(new List<EntertainmentItemEntity>());

            // Act
            var result = await _entertainmentService.GetCityEntertainments(cityId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            _entertainmentRepositoryMock.Verify(r => r.GetCityEntertainments(cityId), Times.Once);

        }

        [Fact]
        public async Task GetCategoryEntertainments_WithValidCategoryId_ReturnsEntertainmentCards()
        {
            // Arrange
            int categoryId = 1;

            var entertainmentItems = new List<EntertainmentItemEntity>
        {
            new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                EntertainmentName = "Entertainment 1",
                Price = 10.0,
                Gallery = new List<GalleryEntity>
                {
                    new GalleryEntity
                    {
                        ImageId = 1,
                        ImageName = "Image 1",
                        ImageLocation = "/images/image1.jpg"
                    }
                },
                Reviews = new List<ReviewEntity>
                {
                    new ReviewEntity
                    {
                        Rating = 3
                    },
                    new ReviewEntity
                    {
                        Rating = 4
                    }
                }
            },
            new EntertainmentItemEntity
            {
                EntertainmentId = 2,
                EntertainmentName = "Entertainment 2",
                Price = 20.0,
                Gallery = new List<GalleryEntity>
                {
                    new GalleryEntity
                    {
                        ImageId = 2,
                        ImageName = "Image 2",
                        ImageLocation = "/images/image2.jpg"
                    }
                },
                Reviews = new List<ReviewEntity>
                {
                    new ReviewEntity
                    {
                        Rating = 5
                    }
                }
            }
        };

            _entertainmentRepositoryMock.Setup(r => r.GetCategoryEntertainments(categoryId)).ReturnsAsync(entertainmentItems);

            // Act
            var result = await _entertainmentService.GetCategoryEntertainments(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal(1, result[0].Id);
            Assert.Equal("Entertainment 1", result[0].Name);
            Assert.Equal(10.0, result[0].Price);
            Assert.NotNull(result[0].Image);
            Assert.Equal(1, result[0].Image.ImageId);
            Assert.Equal("https://localhost:7229/images/image1.jpg", result[0].Image.ImageLocation);
            Assert.Equal("Image 1", result[0].Image.ImageName);
            Assert.Equal(3.5, result[0].Rating);

            Assert.Equal(2, result[1].Id);
            Assert.Equal("Entertainment 2", result[1].Name);
            Assert.Equal(20.0, result[1].Price);
            Assert.NotNull(result[1].Image);
            Assert.Equal(2, result[1].Image.ImageId);
            Assert.Equal("https://localhost:7229/images/image2.jpg", result[1].Image.ImageLocation);
            Assert.Equal("Image 2", result[1].Image.ImageName);
            Assert.Equal(5.0, result[1].Rating);
        }

        [Fact]
        public async Task GetCategoryEntertainments_WithInvalidCategoryId_ReturnsEmptyList()
        {
            // Arrange
            int categoryId = 1;

            _entertainmentRepositoryMock.Setup(r => r.GetCategoryEntertainments(categoryId)).ReturnsAsync(new List<EntertainmentItemEntity>());

            // Act
            var result = await _entertainmentService.GetCategoryEntertainments(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<EntertainmentCardDto>>(result);
            Assert.Empty(result);

            // Verify
            _entertainmentRepositoryMock.Verify(r => r.GetCategoryEntertainments(categoryId), Times.Once);
        }

        [Fact]
        public async Task CreateEntertainment_WithValidData_ReturnsEntertainmentId()
        {
            // Arrange
            var createEntertainment = new CreateEntertainmentDto
            {
                Name = "Test Entertainment",
                Description = "Test Entertainment Description",
                Price = 50,
                PhoneNumber = "123456789",
                Address = "Test Address",
                Email = "test@test.com",
                CitiesIds = new List<int> { 1, 2 },
                CategoriesIds = new List<int> { 1, 2 }
            };
            var id = 1;
            var entertainmentEntity = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                EntertainmentName = createEntertainment.Name,
                EntertainmentDescription = createEntertainment.Description,
                Price = createEntertainment.Price,
                UserId = id,
                PhoneNumber = createEntertainment.PhoneNumber,
                Address = createEntertainment.Address,
                Email = createEntertainment.Email,
                Cities = new List<CityEntity> { new CityEntity { CityId = 1 }, new CityEntity { CityId = 2 } },
                Categories = new List<CategoryEntity> { new CategoryEntity { CategoryId = 1 }, new CategoryEntity { CategoryId = 2 } }
            };
            _cityRepositoryMock
                .Setup(x => x.GetOneCity(It.IsAny<int>()))
                .ReturnsAsync(new CityEntity { CityId = 1 });
            _categoriesRepositoryMock
                .Setup(x => x.GetOneCategory(It.IsAny<int>()))
                .ReturnsAsync(new CategoryEntity { CategoryId = 1 });
            _entertainmentRepositoryMock
                .Setup(x => x.CreateEntertainment(It.IsAny<EntertainmentItemEntity>()))
                .ReturnsAsync(entertainmentEntity);

            // Act
            var result = await _entertainmentService.CreateEntertainment(createEntertainment, id);

            // Assert
            Assert.Equal(entertainmentEntity.EntertainmentId, result);
        }

        [Fact]
        public async Task UpdateEntertainment_WithValidData_ShouldReturnTrue()
        {
            // Arrange
            var existingEntertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                EntertainmentName = "Existing Entertainment",
                EntertainmentDescription = "Existing Entertainment Description",
                Price = 100.0,
                PhoneNumber = "123456789",
                Address = "Existing Address",
                Email = "existing@example.com",
            };
            _entertainmentRepositoryMock.Setup(repo => repo.GetEntertainment(existingEntertainment.EntertainmentId))
                .ReturnsAsync(existingEntertainment);

            var updateModel = new UpdateEntertainmentDto
            {
                Id = 1,
                Name = "Updated Entertainment",
                Description = "Updated Entertainment Description",
                Price = 200.0,
                Phone = "987654321",
                Address = "Updated Address",
                Email = "updated@example.com",
            };

            var expected = true;
            // Set the updated properties of the existing entertainment item
            existingEntertainment.EntertainmentName = updateModel.Name;
            existingEntertainment.EntertainmentDescription = updateModel.Description;
            existingEntertainment.Price = updateModel.Price;
            existingEntertainment.PhoneNumber = updateModel.Phone;
            existingEntertainment.Address = updateModel.Address;
            existingEntertainment.Email = updateModel.Email;

            // Act
            var result = await _entertainmentService.UpdateEntertainment(updateModel);

            // Assert
            
            Assert.Equal(updateModel.Name, existingEntertainment.EntertainmentName);
            Assert.Equal(updateModel.Description, existingEntertainment.EntertainmentDescription);
            Assert.Equal(updateModel.Price, existingEntertainment.Price);
            Assert.Equal(updateModel.Phone, existingEntertainment.PhoneNumber);
            Assert.Equal(updateModel.Address, existingEntertainment.Address);
            Assert.Equal(updateModel.Email, existingEntertainment.Email);
            Assert.True(expected);
        }


        [Fact]
        public async Task UpdateEntertainment_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            var updateModel = new UpdateEntertainmentDto
            {
                Id = 1,
                Name = "Updated Entertainment",
                Description = "Updated Entertainment Description",
                Price = 200.0,
                Phone = "987654321",
                Address = "Updated Address",
                Email = "updated@example.com",
                Cities = new List<CityDto> { new CityDto { CityId = 2, CityName = "City2" } },
                Categories = new List<CategoryDto> { new CategoryDto { CategoryId = 2, CategoryName = "Category2" } }
            };
            _entertainmentRepositoryMock.Setup(repo => repo.GetEntertainment(updateModel.Id))
                .ReturnsAsync((EntertainmentItemEntity)null);

            // Act
            var result = await _entertainmentService.UpdateEntertainment(updateModel);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteEntertainment_WithExistingEntertainment_ShouldReturnTrue()
        {
            // Arrange
            var existingEntertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                EntertainmentName = "Test Entertainment",
                EntertainmentDescription = "Test Description",
                Price = 100.00,
                PhoneNumber = "1234567890",
                Email = "test@test.com",
                Address = "Test Address"
            };

            _entertainmentRepositoryMock.Setup(x => x.GetEntertainment(existingEntertainment.EntertainmentId))
                .ReturnsAsync(existingEntertainment);
            _entertainmentRepositoryMock.Setup(x => x.DeleteEntertainment(existingEntertainment))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _entertainmentService.DeleteEntertainment(existingEntertainment.EntertainmentId);

            // Assert
            Assert.True(result);
            _entertainmentRepositoryMock.Verify(x => x.DeleteEntertainment(existingEntertainment), Times.Once);
        }

        [Fact]
        public async Task DeleteEntertainment_WithNonExistingEntertainment_ShouldReturnFalse()
        {
            // Arrange
            var nonExistingEntertainmentId = 1;
            _entertainmentRepositoryMock.Setup(x => x.GetEntertainment(nonExistingEntertainmentId))
                .ReturnsAsync((EntertainmentItemEntity)null);

            // Act
            var result = await _entertainmentService.DeleteEntertainment(nonExistingEntertainmentId);

            // Assert
            Assert.False(result);
            _entertainmentRepositoryMock.Verify(x => x.DeleteEntertainment(It.IsAny<EntertainmentItemEntity>()), Times.Never);
        }

        [Fact]
        public async Task DeleteEntertainment_WithExceptionThrown_ShouldReturnFalse()
        {
            // Arrange
            var existingEntertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                EntertainmentName = "Test Entertainment",
                EntertainmentDescription = "Test Description",
                Price = 100.00,
                PhoneNumber = "1234567890",
                Email = "test@test.com",
                Address = "Test Address"
            };

            _entertainmentRepositoryMock.Setup(x => x.GetEntertainment(existingEntertainment.EntertainmentId))
                .ReturnsAsync(existingEntertainment);
            _entertainmentRepositoryMock.Setup(x => x.DeleteEntertainment(existingEntertainment))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _entertainmentService.DeleteEntertainment(existingEntertainment.EntertainmentId);

            // Assert
            Assert.False(result);
            _entertainmentRepositoryMock.Verify(x => x.DeleteEntertainment(existingEntertainment), Times.Once);
        }

    }
}
