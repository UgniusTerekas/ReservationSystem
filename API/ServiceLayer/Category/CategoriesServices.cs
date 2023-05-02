using DataLayer.Entities.Category;
using DataLayer.Interfaces;
using DataLayer.Repositories.Category;
using Microsoft.AspNetCore.Http;
using ModelLayer.Contracts.Category;
using ModelLayer.Dto.Category;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Contracts.Category;

namespace ServiceLayer.CategoryService
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesServices(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            var categories = await _categoriesRepository.GetCategories();

            var categoriesDto = categories
                 .Select(c => new CategoryDto
                 {
                     CategoryId = c.CategoryId,
                     CategoryName = c.CategoryName,
                     CategoryImage = c.CategoryImage,
                 })
                 .ToList();

            return categoriesDto;
        }

        public async Task<CategoryDto> GetCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                return null;
            }

            var category = await _categoriesRepository.GetOneCategory(categoryId);

            if (category == null)
            {
                return null;
            }

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage,
            };
        }

        public async Task<CategoryResponse> CreateCategory(CategoryRequest category)
        {
            var imageBytes = await ValidateImageAsync(category.CategoryImage);

            if (imageBytes == null)
            {
                return null;
            }

            var newCategory = new CategoryEntity
            {
                CategoryName = category.CategoryName,
                CategoryImage = imageBytes,
            };

            await _categoriesRepository.CreateCategory(newCategory);

            return new CategoryResponse
            {
                CategoryName = newCategory.CategoryName,
                CategoryImage = newCategory.CategoryImage,
            };
        }

        public async Task<CategoryDto> UpdateCategory(UpdateCategoryRequest category, IFormFile file)
        {
            var existingCategory = await _categoriesRepository.GetOneCategory(category.CategoryId);

            if (existingCategory == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(category.CategoryName))
            {
                existingCategory.CategoryName = category.CategoryName;
            }

            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);

                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                {
                    throw new ArgumentException("Invalid file format. Only JPG and PNG files are allowed.");
                }

                existingCategory.CategoryImage = file.FileName;
            }

            await _categoriesRepository.UpdateCategory(existingCategory);

            return new CategoryDto
            {
                CategoryId = existingCategory.CategoryId,
                CategoryName = existingCategory.CategoryName,
                CategoryImage = existingCategory.CategoryImage,
            };
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await _categoriesRepository.GetOneCategory(categoryId);

            if (category == null) 
            {
                return false;
            }

            await _categoriesRepository.DeleteCategory(category);

            return true;
        }

        private async Task<string> ValidateImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file format.");
            }

            if (file.ContentType != "image/png"
                && file.ContentType != "image/jpeg")
            {
                throw new ArgumentException("Invalid file format.");
            }

            return file.FileName;
        }
    }
}
