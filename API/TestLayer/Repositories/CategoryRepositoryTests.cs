using DataLayer;
using DataLayer.Entities.Category;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Repositories.Category;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Repositories
{
    public class CategoryRepositoryTests
    {
        private readonly DataBaseContext _dbContext;
        private readonly CategoriesRepository _categoryRepository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
        .UseInMemoryDatabase(databaseName: "TestDB");

        public CategoryRepositoryTests()
        {
            // Create a new instance of the ApplicationDbContext and CategoryRepository
            // for each test method
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "test_database")
                .Options;
            _dbContext = new DataBaseContext(options);
            _categoryRepository = new CategoriesRepository(_dbContext);
        }

        [Fact]
        public async Task CreateCategory_WithValidCategory_ReturnsCreatedCategory()
        {
            // Arrange
            var category = new CategoryEntity
            {
                CategoryName = "Test category",
                CategoryImage = "test.jpg"
            };

            // Act
            var result = await _categoryRepository.CreateCategory(category);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category.CategoryName, result.CategoryName);
            Assert.Equal(category.CategoryImage, result.CategoryImage);
        }

        [Fact]
        public async Task GetOneCategory_WithValidCategoryId_ReturnsCategoryEntity()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetOneCategory_WithValidCategoryId_ReturnsCategoryEntity")
                .Options;
            var dbContext = new DataBaseContext(options);
            var categoryRepository = new CategoriesRepository(dbContext);
            var categoryId = 1;
            var categoryEntity = new CategoryEntity
            {
                CategoryId = categoryId,
                CategoryName = "Test Category",
                CategoryImage = "test_category_image.jpg"
            };
            await dbContext.Categories.AddAsync(categoryEntity);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await categoryRepository.GetOneCategory(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categoryId, result.CategoryId);
            Assert.Equal(categoryEntity.CategoryName, result.CategoryName);
            Assert.Equal(categoryEntity.CategoryImage, result.CategoryImage);
        }

        [Fact]
        public async Task GetOneCategory_WithInvalidCategoryId_ReturnsNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DataBaseContext>()
                .UseInMemoryDatabase(databaseName: "GetOneCategory_WithInvalidCategoryId_ReturnsNull")
                .Options;
            var dbContext = new DataBaseContext(options);
            var categoryRepository = new CategoriesRepository(dbContext);
            var categoryId = 1;
            var categoryEntity = new CategoryEntity
            {
                CategoryId = categoryId,
                CategoryName = "Test Category",
                CategoryImage = "test_category_image.jpg"
            };
            await dbContext.Categories.AddAsync(categoryEntity);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await categoryRepository.GetOneCategory(categoryId + 1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCategories_WithNoCategories_ReturnsEmptyList()
        {
            // Arrange
            var categoryRepository = new CategoriesRepository(_dbContext);

            // Act
            var result = await categoryRepository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoryEntity>>(result);
        }

        [Fact]
        public async Task GetCategories_WithCategories_ReturnsList()
        {
            // Arrange
            var category1 = new CategoryEntity { CategoryName = "Category 1", CategoryImage = "image1.jpg" };
            var category2 = new CategoryEntity { CategoryName = "Category 2", CategoryImage = "image2.jpg" };
            await _dbContext.Categories.AddRangeAsync(category1, category2);
            await _dbContext.SaveChangesAsync();

            var categoryRepository = new CategoriesRepository(_dbContext);

            // Act
            var result = await categoryRepository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoryEntity>>(result);
        }

        [Fact]
        public async Task DeleteCategory_WithNullCategory_ThrowsArgumentNullException()
        {
            // Arrange
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            var categoryRepository = new CategoriesRepository(dbContext);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => categoryRepository.DeleteCategory(null));
        }

        [Fact]
        public async Task UpdateCategory_WithValidCategory_ReturnsUpdatedCategory()
        {
            // Arrange
            var dbContext = new DataBaseContext(_optionsBuilder.Options);
            var categoryRepository = new CategoriesRepository(dbContext);

            var category = new CategoryEntity { CategoryName = "Category 1", CategoryImage="sss.png" };
            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            category.CategoryName = "Updated Category 1";

            // Act
            var result = await categoryRepository.UpdateCategory(category);

            // Assert
            Assert.Equal(category, result);
            Assert.Equal("Updated Category 1", result.CategoryName);
        }
    }
}
