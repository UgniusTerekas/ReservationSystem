using DataLayer;
using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Entities.Gallery;
using DataLayer.Entities.Review;
using DataLayer.Entities.User;
using DataLayer.Repositories.Entertainment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Repositories
{
    public class EntertainmentRepositoryTests
    {
        private readonly DbContextOptions<DataBaseContext> _options;

        public EntertainmentRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task GetEntertainments_ReturnsListOfEntertainmentItems()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetEntertainments_ReturnsListOfEntertainmentItems")
                .Options;
            var dbContext = new DataBaseContext(options);

            dbContext.Entertainments.Add(new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 1",
                Address = "ss",
                EntertainmentDescription = "sss",
                PhoneNumber = "13465",
                Email = "sss@gmail.com"
            });
            dbContext.Entertainments.Add(new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 2",
                Address = "ss",
                EntertainmentDescription = "sss",
                PhoneNumber = "13465",
                Email = "sss@gmail.com"
            });
            dbContext.SaveChanges();

            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainments();

            // Assert
            Assert.IsType<List<EntertainmentItemEntity>>(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Entertainment 1", result[0].EntertainmentName);
            Assert.Equal("Entertainment 2", result[1].EntertainmentName);
        }

        [Fact]
        public async Task GetEntertainments_IncludesGallery()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetEntertainments_IncludesGallery")
                .Options;
            var dbContext = new DataBaseContext(options);

            dbContext.Entertainments.Add(new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 1",
                EntertainmentDescription = "Description",
                Price = 10.0,
                PhoneNumber = "123-456-7890",
                Address = "123 Main St",
                Email = "example@example.com",
                Gallery = new List<GalleryEntity>
        {
            new GalleryEntity { ImageName = "image1.jpg", ImageLocation = "sss" },
            new GalleryEntity { ImageName = "image2.jpg" , ImageLocation = "sss"}
        }
            });
            dbContext.SaveChanges();

            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainments();

            // Assert
            Assert.Equal(2, result[0].Gallery.Count);
            Assert.Equal("image1.jpg", result[0].Gallery.ElementAt(0).ImageName);
            Assert.Equal("image2.jpg", result[0].Gallery.ElementAt(1).ImageName);
        }


        [Fact]
        public async Task GetEntertainments_IncludesReviews()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetEntertainments_IncludesReviews")
                .Options;
            var dbContext = new DataBaseContext(options);

            dbContext.Entertainments.Add(new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 1",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                Reviews = new List<ReviewEntity>
            {
                new ReviewEntity { Rating = 5, Review = "sss" },
                new ReviewEntity { Rating = 3, Review = "sss" },
                new ReviewEntity { Rating = 4, Review = "sss" }
            }
            });
            dbContext.SaveChanges();

            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainments();

            // Assert
            Assert.Equal(3, result[0].Reviews.Count);
            Assert.Equal(5, result[0].Reviews.ElementAt(0).Rating);
            Assert.Equal(3, result[0].Reviews.ElementAt(1).Rating);
            Assert.Equal(4, result[0].Reviews.ElementAt(2).Rating);
        }

        [Fact]
        public async Task GetEntertainments_IncludesCategories()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "GetEntertainments_IncludesCategories")
            .Options;
            var dbContext = new DataBaseContext(options);

            dbContext.Categories.Add(new CategoryEntity { CategoryName = "Category 1", CategoryImage = "sss" });
            dbContext.Categories.Add(new CategoryEntity { CategoryName = "Category 2", CategoryImage = "sss" });
            dbContext.SaveChanges();

            dbContext.Entertainments.Add(new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 1",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                Categories = new List<CategoryEntity>
    {
        new CategoryEntity { CategoryName = "Category 1", CategoryImage = "sss" },
        new CategoryEntity { CategoryName = "Category 2", CategoryImage = "sss" }
    }
            });
            dbContext.SaveChanges();

            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainments();

            // Assert
            Assert.Equal(2, result[0].Categories.Count);
            Assert.Equal("Category 1", result[0].Categories.ElementAt(0).CategoryName);
            Assert.Equal("Category 2", result[0].Categories.ElementAt(1).CategoryName);

        }

        [Fact]
        public async Task GetCityEntertainments_ReturnsOnlyEntertainmentsInSpecifiedCity()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var repository = new EntertainmentRepository(dbContext);

            dbContext.Entertainments.Add(new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 1",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                Cities = new List<CityEntity>
            {
                new CityEntity { CityId = 1, CityImage = "sss.png", CityName = "Vln" }
            }
            });
            dbContext.Entertainments.Add(new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 2",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                Cities = new List<CityEntity>
            {
                new CityEntity { CityId = 2 , CityImage = "sss.png", CityName = "Vln"}
            }
            });
            dbContext.SaveChanges();

            // Act
            var result = await repository.GetCityEntertainments(1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Entertainment 1", result[0].EntertainmentName);
        }

        [Fact]
        public async Task GetEntertainment_ReturnsEntertainmentItem_WhenEntertainmentExists()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var expectedEntertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "Entertainment 1"
            };
            dbContext.Entertainments.Add(expectedEntertainment);
            await dbContext.SaveChangesAsync();
            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainment(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEntertainment.EntertainmentId, result.EntertainmentId);
            Assert.Equal(expectedEntertainment.EntertainmentName, result.EntertainmentName);
        }

        [Fact]
        public async Task GetEntertainment_ReturnsNull_WhenEntertainmentDoesNotExist()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainment(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEntertainment_ReturnsEntertainmentItemWithGallery_WhenEntertainmentHasGallery()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            await dbContext.Database.EnsureDeletedAsync();
            var expectedEntertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "Entertainment 1",
                Gallery = new List<GalleryEntity>
            {
                new GalleryEntity { ImageId = 1, ImageLocation = "image1.png", ImageName = "sss" },
                new GalleryEntity { ImageId = 2, ImageLocation = "image2.png", ImageName = "sss" }
            }
            };
            dbContext.Entertainments.Add(expectedEntertainment);
            await dbContext.SaveChangesAsync();
            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainment(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEntertainment.EntertainmentId, result.EntertainmentId);
            Assert.Equal(expectedEntertainment.EntertainmentName, result.EntertainmentName);
            Assert.Equal(expectedEntertainment.Gallery.Count, result.Gallery.Count);
        }

        [Fact]
        public async Task GetEntertainment_ReturnsEntertainmentItemWithCities_WhenEntertainmentHasCities()
        {
            // Arrange
            using var dbContext = new DataBaseContext(_options);
            await dbContext.Database.EnsureDeletedAsync();
            var expectedEntertainment = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                EntertainmentName = "Entertainment 1",
                Cities = new List<CityEntity>
            {
                new CityEntity { CityId = 1, CityName = "City 1", CityImage = "sss.png"},
                new CityEntity { CityId = 2, CityName = "City 2", CityImage = "sss.png" }
            }
            };
            dbContext.Entertainments.Add(expectedEntertainment);
            await dbContext.SaveChangesAsync();
            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainment(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEntertainment.EntertainmentId, result.EntertainmentId);
        }

        [Fact]
        public async Task CreateEntertainment_AddsEntertainmentToDbContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "CreateEntertainment_AddsEntertainmentToDbContext")
                .Options;
            var dbContext = new DataBaseContext(options);
            var repository = new EntertainmentRepository(dbContext);

            var entertainment = new EntertainmentItemEntity
            {
                EntertainmentName = "Test Entertainment",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
            };

            // Act
            await repository.CreateEntertainment(entertainment);

            // Assert
            Assert.Equal(1, dbContext.Entertainments.Count());
            Assert.Contains(entertainment, dbContext.Entertainments);
        }

        [Fact]
        public async Task CreateEntertainment_SetsEntertainmentId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "CreateEntertainment_SetsEntertainmentId")
                .Options;
            var dbContext = new DataBaseContext(options);
            var repository = new EntertainmentRepository(dbContext);

            var entertainment = new EntertainmentItemEntity
            {
                EntertainmentName = "Test Entertainment",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
            };

            // Act
            await repository.CreateEntertainment(entertainment);

            // Assert
            Assert.NotEqual(0, entertainment.EntertainmentId);
        }

        [Fact]
        public async Task CreateEntertainment_SavesChangesToDbContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "CreateEntertainment_SavesChangesToDbContext")
                .Options;
            var dbContext = new DataBaseContext(options);
            var repository = new EntertainmentRepository(dbContext);

            var entertainment = new EntertainmentItemEntity
            {
                EntertainmentName = "Test Entertainment",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
            };

            // Act
            await repository.CreateEntertainment(entertainment);

            // Assert
            Assert.Equal(EntityState.Unchanged, dbContext.Entry(entertainment).State);
        }

        [Fact]
        public async Task UpdateEntertainment_ShouldUpdateEntertainmentAndReturnIt()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "UpdateEntertainment_ShouldUpdateEntertainmentAndReturnIt")
                .Options;
            var dbContext = new DataBaseContext(options);

            var entertainment = new EntertainmentItemEntity
            {
                EntertainmentName = "Entertainment 1",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
            };
            dbContext.Entertainments.Add(entertainment);
            dbContext.SaveChanges();

            var repository = new EntertainmentRepository(dbContext);

            // Act
            entertainment.EntertainmentName = "Entertainment 1 updated";
            entertainment.EntertainmentDescription = "Description 1 updated";

            var updatedEntertainment = await repository.UpdateEntertainment(entertainment);

            // Assert
            Assert.NotNull(updatedEntertainment);
            Assert.Equal(entertainment.EntertainmentId, updatedEntertainment.EntertainmentId);
            Assert.Equal("Entertainment 1 updated", updatedEntertainment.EntertainmentName);

            // Detach entity from dbContext
            dbContext.Entry(entertainment).State = EntityState.Detached;
        }

        [Fact]
        public async Task GetEntertainmentsForEdit_ReturnsEntertainmentItems_ForAdminId()
        {
            var dbContext = new DataBaseContext(_options);
            // Arrange
            var admin = new UserEntity
            {
                UserId = 1,
                UserName = "admin",
                UserEmail = "admin@example.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var entertainment1 = new EntertainmentItemEntity
            {
                EntertainmentId = 1,
                EntertainmentName = "Entertainment 1",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                UserId = admin.UserId
            };
            var entertainment2 = new EntertainmentItemEntity
            {
                EntertainmentId = 2,
                EntertainmentName = "Entertainment 2"
                ,
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                UserId = admin.UserId
            };
            var entertainment3 = new EntertainmentItemEntity
            {
                EntertainmentId = 3,
                EntertainmentName = "Entertainment 3",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
                UserId = 2
            };

            dbContext.Users.Add(admin);
            
            dbContext.Entertainments.Add(entertainment1);
            dbContext.Entertainments.Add(entertainment2);
            dbContext.Entertainments.Add(entertainment3);
            dbContext.SaveChanges();
            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainmentsForEdit(admin.UserId);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Entertainment 1", result[0].EntertainmentName);
            Assert.Equal("Entertainment 2", result[1].EntertainmentName);
        }


        [Fact]
        public async Task GetEntertainmentsForEdit_ReturnsEmptyList_WhenAdminHasNoEntertainments()
        {
            // Arrange
            var admin = new UserEntity { UserId = 1, UserName = "admin", UserEmail = "admin@admin.com",
                PasswordSalt = Encoding.UTF8.GetBytes("salt"),
                Password = Encoding.UTF8.GetBytes("password")
            };
            var dbContext = new DataBaseContext(_options);
            await dbContext.Database.EnsureDeletedAsync();
            dbContext.Users.Add(admin);
            dbContext.SaveChanges();
            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainmentsForEdit(admin.UserId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetEntertainmentsForEdit_ReturnsEmptyList_WhenAdminIdIsInvalid()
        {
            // Arrange
            var dbContext = new DataBaseContext(_options);
            await dbContext.Database.EnsureDeletedAsync();
            var repository = new EntertainmentRepository(dbContext);

            // Act
            var result = await repository.GetEntertainmentsForEdit(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task DeleteEntertainment_RemovesEntertainmentFromDatabase()
        {
            // Arrange
            var dbContext = new DataBaseContext(_options);
            var entertainment = new EntertainmentItemEntity { 
                EntertainmentId = 1, EntertainmentName = "Entertainment 1",
                Address = "fdsf",
                Email = "email@gmail.com",
                PhoneNumber = "12345",
                EntertainmentDescription = "sfdds",
            };
            dbContext.Entertainments.Add(entertainment);
            dbContext.SaveChanges();
            var repository = new EntertainmentRepository(dbContext);

            // Act
            await repository.DeleteEntertainment(entertainment);

            // Assert
            var result = await dbContext.Entertainments.FirstOrDefaultAsync(e => e.EntertainmentId == entertainment.EntertainmentId);
            Assert.Null(result);
            await dbContext.Database.EnsureDeletedAsync();
        }
    }
}
