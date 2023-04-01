using DataLayer.Entities.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ICategoriesRepository
    {
        Task<CategoryEntity> CreateCategory(CategoryEntity category);

        Task<CategoryEntity> GetOneCategory(int categoryId);

        Task<List<CategoryEntity>> GetCategories();

        Task<CategoryEntity> UpdateCategory(CategoryEntity category);

        Task DeleteCategory(CategoryEntity category);
    }
}
