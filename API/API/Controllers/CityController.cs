using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Contracts.Category;
using ModelLayer.Contracts.City;
using ModelLayer.Dto.Category;
using ModelLayer.Dto.City;
using ServiceLayer.CategoryService;
using ServiceLayer.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICitiesServices _citiesServices;

        public CityController(ICitiesServices citiesServices)
        {
            _citiesServices = citiesServices;
        }

        [HttpGet("getCities")]
        [ProducesResponseType(typeof(List<CityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCities()
        {
            var result = await _citiesServices.GetCities();

            return Ok(result);
        }

        [HttpGet("getCity")]
        [ProducesResponseType(typeof(CityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCity(int categoryId)
        {
            var result = await _citiesServices.GetCity(categoryId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("createCity")]
        [ProducesResponseType(typeof(CityDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCity(string name, IFormFile file)
        {
            var city = new CityRequest
            {
                CityName = name,
                CityImage = file
            };

            var result = await _citiesServices.CreateCity(city);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("updateCity")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCity(int id, string name, IFormFile? file)
        {
            var city = new UpdateCityRequest
            {
                CityId = id,
                CityName = name,
                CityImage = file
            };

            var result = await _citiesServices.UpdateCity(city, file);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("deleteCity")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCategory(int cityId)
        {
            var result = await _citiesServices.DeleteCity(cityId);

            if (result == false)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
