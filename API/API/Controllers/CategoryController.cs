using Microsoft.AspNetCore.Mvc;
using ModelLayer.Contracts.Category;
using ModelLayer.Dto.Category;
using ServiceLayer.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesServices _categoriesServices;

        public CategoryController(ICategoriesServices categoriesServices)
        {
            _categoriesServices = categoriesServices;
        }

        [HttpPost("createCategory")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory(string name, IFormFile file)
        {
            var category = new CategoryRequest
            {
                CategoryName = name,
                CategoryImage = file
            };

            var result = await _categoriesServices.CreateCategory(category);

            if (result == null)
            {
                return BadRequest();
            }

            return Created(string.Empty, result);
        }

        [HttpGet("getCategory")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            var result = await _categoriesServices.GetCategory(categoryId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("getCategories")]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _categoriesServices.GetCategories();

            return Ok(result);
        }

        [HttpPut("updateCategory")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCategory(int id, string name, IFormFile? file)
        {
            var category = new UpdateCategoryRequest
            {
                CategoryId = id,
                CategoryName = name,
                CategoryImage = file
            };

            var result = await _categoriesServices.UpdateCategory(category, file);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("deleteCategory")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var result = await _categoriesServices.DeleteCategory(categoryId);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
