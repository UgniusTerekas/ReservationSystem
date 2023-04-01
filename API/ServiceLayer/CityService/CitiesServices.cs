using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Interfaces;
using DataLayer.Repositories.Category;
using Microsoft.AspNetCore.Http;
using ModelLayer.Contracts.Category;
using ModelLayer.Contracts.City;
using ModelLayer.Dto.Category;
using ModelLayer.Dto.City;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.CityService
{
    public class CitiesServices : ICitiesServices
    {
        private readonly ICityRepository _cityRepository;

        public CitiesServices(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<List<CityDto>> GetCities()
        {
            var cities = await _cityRepository.GetCities();

            var citiesDto = cities
                 .Select(c => new CityDto
                 {
                     CityId = c.CityId,
                     CityName = c.CityName,
                     CityImage = c.CityImage,
                 })
                 .ToList();

            return citiesDto;
        }

        public async Task<CityDto> GetCity(int cityId)
        {
            if (cityId <= 0)
            {
                return null;
            }

            var city = await _cityRepository.GetOneCity(cityId);

            if (city == null)
            {
                return null;
            }

            return new CityDto
            {
                CityId = city.CityId,
                CityName = city.CityName,
                CityImage = city.CityImage
            };
        }

        public async Task<CityDto> CreateCity(CityRequest city)
        {
            var imageBytes = await ValidateImageAsync(city.CityImage);

            if (imageBytes == null)
            {
                return null;
            }

            var newCity = new CityEntity
            {
                CityName = city.CityName,
                CityImage = imageBytes,
            };

            await _cityRepository.CreateCity(newCity);

            return new CityDto
            {
                CityId = newCity.CityId,
                CityName = newCity.CityName,
                CityImage = newCity.CityImage,
            };
        }

        public async Task<CityDto> UpdateCity(UpdateCityRequest city, IFormFile file)
        {
            var existingCity = await _cityRepository.GetOneCity(city.CityId);

            if (existingCity == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(city.CityName))
            {
                existingCity.CityName = city.CityName;
            }

            if (file != null)
            {
                var fileExtension = Path.GetExtension(file.FileName);

                if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png")
                {
                    throw new ArgumentException("Invalid file format. Only JPG and PNG files are allowed.");
                }

                existingCity.CityImage = file.FileName;
            }

            await _cityRepository.UpdateCity(existingCity);

            return new CityDto
            {
                CityId = existingCity.CityId,
                CityName = existingCity.CityName,
                CityImage = existingCity.CityImage,
            };
        }

        public async Task<bool> DeleteCity(int cityId)
        {
            var city = await _cityRepository.GetOneCity(cityId);

            if (city == null)
            {
                return false;
            }

            await _cityRepository.DeleteCity(city);

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
