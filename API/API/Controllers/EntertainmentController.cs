using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Entertainment;
using ServiceLayer.CityService;
using ServiceLayer.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntertainmentController : ControllerBase
    {
        private readonly IEntertainmentServices _entertainmentServices;

        public EntertainmentController(IEntertainmentServices entertainmentServices)
        {
            _entertainmentServices = entertainmentServices;
        }

        [HttpGet("entertainments")]
        [ProducesResponseType(typeof(List<EntertainmentCardDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCities()
        {
            var result = await _entertainmentServices.GetEntertainments();

            return Ok(result);
        }

        [HttpGet("filteredEntertainments")]
        [ProducesResponseType(typeof(List<EntertainmentCardDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCities(int? cityId, int? categoryId)
        {
            var result = await _entertainmentServices.GetEntertainments(cityId, categoryId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost("Entertainment")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEntertainment(CreateEntertainmentDto createEntertainmentDto)
        {
            var result = await _entertainmentServices.CreateEntertainment(createEntertainmentDto);

            if (result == false)
            {
                return BadRequest();
            }

            return Created(string.Empty, true);
        }

        [HttpPatch("Entertainment")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEntertainment(UpdateEntertainmentDto updateEntertainmentDto)
        {
            var result = await _entertainmentServices.UpdateEntertainment(updateEntertainmentDto);

            if (result == false)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpDelete("Entertainment")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteEntertainment(int id)
        {
            var result = await _entertainmentServices.DeleteEntertainment(id);

            if (result == false)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
