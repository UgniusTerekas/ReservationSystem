using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.City;
using ModelLayer.Dto.Entertainment;
using ServiceLayer.CityService;
using ServiceLayer.Interfaces;
using System.Security.Claims;

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
        public async Task<IActionResult> GetEntertainmentList()
        {
            var result = await _entertainmentServices.GetEntertainments();

            return Ok(result);
        }

        [HttpGet("cityFilteredEntertainments")]
        [ProducesResponseType(typeof(List<EntertainmentCardDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityEntertainmentList(int cityId)
        {
            var result = await _entertainmentServices.GetCityEntertainments(cityId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("categoryFilteredEntertainments")]
        [ProducesResponseType(typeof(List<EntertainmentCardDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryEntertainmentList(int categoryId)
        {
            var result = await _entertainmentServices.GetCategoryEntertainments(categoryId);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet("entertainmentDetails")]
        [ProducesResponseType(typeof(List<EntertainmentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntertainmentDetails(int id)
        {
            var result = await _entertainmentServices.GetEntertainmentDetails(id);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPost("Entertainment")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEntertainment(CreateEntertainmentDto createEntertainmentDto)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _entertainmentServices.CreateEntertainment(createEntertainmentDto, id);

            if (result == -1)
            {
                return BadRequest();
            }

            return Created(string.Empty, result);
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

        [Authorize]
        [HttpGet("editEntertainments")]
        [ProducesResponseType(typeof(List<GetEntertainmentForEditing>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntertainmentForEdit()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _entertainmentServices.GetEntertainmentForEditing(id);

            return Ok(result);
        }
    }
}
