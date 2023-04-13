using DataLayer.Entities.City;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Entertainment
{
    public class EntertainmentRepository : IEntertainmentRepository
    {
        private readonly DataBaseContext _dbContext;

        public EntertainmentRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EntertainmentItemEntity>> GetEntertainments()
        {
            return await _dbContext
                .Entertainments
                .Include(x => x.Gallery)
                .Include(x => x.Reviews)
                .Include(x => x.Categories)
                .Include(x => x.Cities)
                .ToListAsync();
        }

        public async Task<List<EntertainmentItemEntity>> GetEntertainments(int? cityId, int? categoryId)
        {
            var query = _dbContext
                .Entertainments
                .Include(x => x.Gallery)
                .Include(x => x.Reviews)
                .Include(x => x.Categories)
                .Include(x => x.Cities)
                .AsQueryable();

            if (cityId != null)
            {
                query = query.Where(e => e.Cities.Any(c => c.CityId == cityId));
            }

            if (categoryId != null)
            {
                query = query.Where(e => e.Categories.Any(c => c.CategoryId == categoryId));
            }

            return await query.ToListAsync();
        }

        public async Task<EntertainmentItemEntity> GetEntertainment(int entertainmentId)
        {
            return await _dbContext.Entertainments
                .Include(e => e.Gallery)
                .Include(e => e.Cities)
                .Include(e => e.Categories)
                .Include(e => e.Reviews)
                .FirstOrDefaultAsync(e => 
                    e.EntertainmentId == entertainmentId);
        }

        public async Task<EntertainmentItemEntity> CreateEntertainment(EntertainmentItemEntity entertainment)
        {
            _dbContext.Entertainments.Add(entertainment);

            await _dbContext.SaveChangesAsync();

            return entertainment;
        }

        public async Task<EntertainmentItemEntity> UpdateEntertainment(EntertainmentItemEntity entertainment)
        {
            _dbContext.Entry(entertainment).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return entertainment;
        }

        public async Task DeleteEntertainment(EntertainmentItemEntity entertainment)
        {
            _dbContext.Entertainments.Remove(entertainment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
