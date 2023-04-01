using Microsoft.AspNetCore.Http;
using ModelLayer.Contracts.City;
using ModelLayer.Dto.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ICitiesServices
    {
        Task<CityDto> CreateCity(CityRequest city);

        Task<bool> DeleteCity(int cityId);

        Task<List<CityDto>> GetCities();

        Task<CityDto> GetCity(int cityId);

        Task<CityDto> UpdateCity(UpdateCityRequest city, IFormFile file);
    }
}
