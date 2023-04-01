using DataLayer.Entities.Category;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Category
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DataBaseContext _dbContext;

        public CategoriesRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CategoryEntity> CreateCategory(CategoryEntity category)
        {
            _dbContext.Categories.Add(category);

            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task<CategoryEntity> GetOneCategory(int categoryId)
        {
            return await _dbContext.Categories.FindAsync(categoryId);
        }

        public async Task<List<CategoryEntity>> GetCategories()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<CategoryEntity> UpdateCategory(CategoryEntity category)
        {
            _dbContext.Entry(category).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return category;
        }

        public async Task DeleteCategory(CategoryEntity category)
        {
            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();
        }
    }
}
