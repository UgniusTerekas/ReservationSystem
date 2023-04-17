using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.Reservation;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationServices _reservationServices;

        public ReservationController(IReservationServices reservationServices)
        {
            _reservationServices = reservationServices;
        }

        [Authorize]
        [HttpPut("createReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservation)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _reservationServices.CreateReservation(createReservation, id);

            return Ok(result);
        }

        [HttpGet("reservations")]
        [ProducesResponseType(typeof(List<GetReservationsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateReservation()
        {
            var result = await _reservationServices.GetReservations();

            return Ok(result);
        }
    }
}
