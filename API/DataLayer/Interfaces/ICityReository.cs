using DataLayer.Entities.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interfaces
{
    public interface ICityRepository
    {
        Task<CityEntity> CreateCity(CityEntity city);

        Task DeleteCity(CityEntity city);

        Task<List<CityEntity>> GetCities();

        Task<CityEntity> GetOneCity(int cityId);

        Task<CityEntity> UpdateCity(CityEntity city);
    }
}
