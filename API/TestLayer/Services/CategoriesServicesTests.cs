using DataLayer;
using DataLayer.Entities.Category;
using DataLayer.Interfaces;
using DataLayer.Repositories.Category;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Dto.Category;
using Moq;
using ServiceLayer.CategoryService;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Services
{
    public class CategoriesServicesTests
    {
        private readonly Mock<ICategoriesRepository> _categoriesRepositoryMock;
        private readonly CategoriesServices _categoriesService;

        public CategoriesServicesTests()
        {
            _categoriesRepositoryMock = new Mock<ICategoriesRepository>();
            _categoriesService = new CategoriesServices(_categoriesRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCategory_Returns_Null_If_CategoryId_Is_Less_Or_Equal_To_Zero()
        {
            // Arrange
            int categoryId = 0;

            // Act
            var result = await _categoriesService.GetCategory(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCategory_Returns_Null_If_Category_Is_Null()
        {
            // Arrange
            int categoryId = 1;
            CategoryEntity category = null;
            _categoriesRepositoryMock.Setup(repo => repo.GetOneCategory(categoryId)).ReturnsAsync(category);

            // Act
            var result = await _categoriesService.GetCategory(categoryId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCategory_Returns_CategoryDto()
        {
            // Arrange
            int categoryId = 1;
            var category = new CategoryEntity
            {
                CategoryId = categoryId,
                CategoryName = "Test Category",
                CategoryImage = "testcategory.jpg"
            };
            _categoriesRepositoryMock.Setup(repo => repo.GetOneCategory(categoryId)).ReturnsAsync(category);

            // Act
            var result = await _categoriesService.GetCategory(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CategoryDto>(result);
            Assert.Equal(categoryId, result.CategoryId);
            Assert.Equal(category.CategoryName, result.CategoryName);
            Assert.Equal(category.CategoryImage, result.CategoryImage);
        }

        [Fact]
        public async Task DeleteCategory_WithValidCategoryId_ShouldReturnTrue()
        {
            // Arrange
            int categoryId = 1;
            var category = new CategoryEntity { CategoryId = categoryId };
            _categoriesRepositoryMock.Setup(r => r.GetOneCategory(categoryId)).ReturnsAsync(category);

            // Act
            var result = await _categoriesService.DeleteCategory(categoryId);

            // Assert
            Assert.True(result);
            _categoriesRepositoryMock.Verify(r => r.DeleteCategory(category), Times.Once);
        }

        [Fact]
        public async Task DeleteCategory_WithInvalidCategoryId_ShouldReturnFalse()
        {
            // Arrange
            int categoryId = -1;
            _categoriesRepositoryMock.Setup(r => r.GetOneCategory(categoryId)).ReturnsAsync((CategoryEntity)null!);


            // Act
            var result = await _categoriesService.DeleteCategory(categoryId);

            // Assert
            Assert.False(result);
            _categoriesRepositoryMock.Verify(r => r.DeleteCategory(It.IsAny<CategoryEntity>()), Times.Never);
        }

    }
}
