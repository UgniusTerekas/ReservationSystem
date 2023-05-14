using API.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Contracts.Category;
using ModelLayer.Dto.Category;
using Moq;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLayer.Controllers
{
    public class CategoryControllerTests
    {
        private readonly Mock<ICategoriesServices> _mockCategoryService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<ICategoriesServices>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        /*[Fact]
        public async Task CreateCategory_ValidRequest_ReturnsCreated()
        {
            // Arrange
            var categoryName = "Test Category";
            var imageBytes = Encoding.UTF8.GetBytes("test.png");
            var categoryRequest = new CategoryRequest
            {
                CategoryName = categoryName,
                CategoryImage = new FormFile(new MemoryStream(imageBytes), 0, imageBytes.Length, "Test Category", "test.png")
            };
            var createdCategory = new CategoryResponse
            {
                CategoryName = categoryName,
                CategoryImage = "test.png"
            };
            _mockCategoryService.Setup(x => x.CreateCategory(categoryRequest)).ReturnsAsync(createdCategory);

            // Act
            var result = await _controller.CreateCategory(categoryRequest.CategoryName, categoryRequest.CategoryImage);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var categoryDto = Assert.IsType<CategoryDto>(createdAtActionResult.Value);
            Assert.Equal(createdCategory.CategoryName, categoryDto.CategoryName);
            Assert.Equal(createdCategory.CategoryImage, categoryDto.CategoryImage);
        }*/

        [Fact]
        public async Task CreateCategory_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var categoryName = "Test Category";
            var imageBytes = Encoding.UTF8.GetBytes("Test Image");
            var categoryRequest = new CategoryRequest
            {
                CategoryName = categoryName,
                CategoryImage = new FormFile(new MemoryStream(imageBytes), 0, imageBytes.Length, "Test Image", "test.jpg")
            };
            _mockCategoryService.Setup(x => x.CreateCategory(categoryRequest)).ReturnsAsync((CategoryResponse)null);

            // Act
            var result = await _controller.CreateCategory(categoryName, categoryRequest.CategoryImage);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkObjectResultWithListOfCategoryDto_WhenCategoriesExist()
        {
            // Arrange
            var categories = new List<CategoryDto>
    {
        new CategoryDto { CategoryId = 1, CategoryName = "Category 1", CategoryImage = "image1.jpg" },
        new CategoryDto { CategoryId = 2, CategoryName = "Category 2", CategoryImage = "image2.jpg" }
    };

            _mockCategoryService.Setup(x => x.GetCategories()).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<CategoryDto>>(okObjectResult.Value);
            Assert.Equal(categories.Count, resultValue.Count);
            Assert.Equal(categories[0].CategoryId, resultValue[0].CategoryId);
            Assert.Equal(categories[0].CategoryName, resultValue[0].CategoryName);
            Assert.Equal(categories[0].CategoryImage, resultValue[0].CategoryImage);
            Assert.Equal(categories[1].CategoryId, resultValue[1].CategoryId);
            Assert.Equal(categories[1].CategoryName, resultValue[1].CategoryName);
            Assert.Equal(categories[1].CategoryImage, resultValue[1].CategoryImage);
        }

        [Fact]
        public async Task GetCategories_ShouldReturnOkObjectResultWithEmptyList_WhenNoCategoriesExist()
        {
            // Arrange
            var categories = new List<CategoryDto>();

            _mockCategoryService.Setup(x => x.GetCategories()).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetCategories();

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = Assert.IsAssignableFrom<List<CategoryDto>>(okObjectResult.Value);
            Assert.Empty(resultValue);
        }


        [Fact]
        public async Task GetCategory_WithValidCategoryId_ReturnsOkObjectResult()
        {
            // Arrange
            var categoryId = 1;
            var categoryDto = new CategoryDto
            {
                CategoryId = categoryId,
                CategoryName = "Test Category",
                CategoryImage = "test.png",
            };
            _mockCategoryService.Setup(x => x.GetCategory(categoryId)).ReturnsAsync(categoryDto);

            // Act
            var result = await _controller.GetCategory(categoryId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var categoryResult = Assert.IsType<CategoryDto>(okObjectResult.Value);
            Assert.Equal(categoryDto.CategoryId, categoryResult.CategoryId);
            Assert.Equal(categoryDto.CategoryName, categoryResult.CategoryName);
            Assert.Equal(categoryDto.CategoryImage, categoryResult.CategoryImage);
        }

        [Fact]
        public async Task GetCategory_WithInvalidCategoryId_ReturnsBadRequest()
        {
            // Arrange
            var categoryId = 0;

            // Act
            var result = await _controller.GetCategory(categoryId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task GetCategory_WithNonExistingCategoryId_ReturnsBadRequest()
        {
            // Arrange
            var categoryId = 1;
            _mockCategoryService.Setup(x => x.GetCategory(categoryId)).ReturnsAsync((CategoryDto)null);

            // Act
            var result = await _controller.GetCategory(categoryId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        /*[Fact]
        public async Task UpdateCategory_ValidRequest_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var name = "Test Category";
            var fileBytes = Encoding.UTF8.GetBytes("test.png");
            var file = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "Test Category", "test.png");
            var request = new UpdateCategoryRequest
            {
                CategoryId = id,
                CategoryName = name,
                CategoryImage = file
            };
            var dto = new CategoryDto
            {
                CategoryId = id,
                CategoryName = name,
                CategoryImage = "test.png"
            };
            _mockCategoryService.Setup(x => x.UpdateCategory(request, file)).ReturnsAsync(dto);

            // Act
            var result = await _controller.UpdateCategory(id, name, file);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultDto = Assert.IsType<CategoryDto>(okResult.Value);
            Assert.Equal(dto.CategoryId, resultDto.CategoryId);
            Assert.Equal(dto.CategoryName, resultDto.CategoryName);
            Assert.Equal(dto.CategoryImage, resultDto.CategoryImage);
        }*/

        [Fact]
        public async Task UpdateCategory_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var id = 0;
            var name = "Test Category";
            var fileBytes = Encoding.UTF8.GetBytes("test.png");
            var file = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "Test Category", "test.png");
            var request = new UpdateCategoryRequest
            {
                CategoryId = id,
                CategoryName = name,
                CategoryImage = file
            };
            _mockCategoryService.Setup(x => x.UpdateCategory(request, file)).ReturnsAsync(() => null);

            // Act
            var result = await _controller.UpdateCategory(id, name, file);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task UpdateCategory_InvalidFileFormat_ReturnsBadRequest()
        {
            // Arrange
            var id = 1;
            var name = "Test Category";
            var fileBytes = Encoding.UTF8.GetBytes("test.txt");
            var file = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "Test Category", "test.txt");
            var request = new UpdateCategoryRequest
            {
                CategoryId = id,
                CategoryName = name,
                CategoryImage = file
            };
            _mockCategoryService.Setup(x => x.UpdateCategory(request, file)).ThrowsAsync(new ArgumentException("Invalid file format. Only JPG and PNG files are allowed."));

            // Act
            var result = await _controller.UpdateCategory(id, name, file);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCategory_ValidCategoryId_ReturnsOk()
        {
            // Arrange
            int categoryId = 1;
            _mockCategoryService.Setup(x => x.DeleteCategory(categoryId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteCategory(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCategory_InvalidCategoryId_ReturnsBadRequest()
        {
            // Arrange
            int categoryId = 0;
            _mockCategoryService.Setup(x => x.DeleteCategory(categoryId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteCategory(categoryId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }
    }
}
