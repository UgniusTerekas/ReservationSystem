using Microsoft.AspNetCore.Http;
using ModelLayer.Contracts.Category;
using ModelLayer.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ICategoriesServices
    {
        Task<CategoryResponse> CreateCategory(CategoryRequest category);

        Task<CategoryDto> GetCategory(int categoryId);

        Task<List<CategoryDto>> GetCategories();

        Task<CategoryDto> UpdateCategory(UpdateCategoryRequest category, IFormFile file);

        Task<bool> DeleteCategory(int categoryId);
    }
}
