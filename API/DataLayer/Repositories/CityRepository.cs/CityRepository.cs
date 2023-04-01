using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.CityRepository.cs
{
    public class CityRepository : ICityRepository
    {
        private readonly DataBaseContext _dbContext;

        public CityRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CityEntity>> GetCities()
        {
            return await _dbContext.Cities.ToListAsync();
        }

        public async Task<CityEntity> GetOneCity(int cityId)
        {
            return await _dbContext.Cities.FindAsync(cityId);
        }

        public async Task<CityEntity> CreateCity(CityEntity city)
        {
            _dbContext.Cities.Add(city);

            await _dbContext.SaveChangesAsync();

            return city;
        }

        public async Task<CityEntity> UpdateCity(CityEntity city)
        {
            _dbContext.Entry(city).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();

            return city;
        }

        public async Task DeleteCity(CityEntity city)
        {
            _dbContext.Cities.Remove(city);

            await _dbContext.SaveChangesAsync();
        }
    }
}
