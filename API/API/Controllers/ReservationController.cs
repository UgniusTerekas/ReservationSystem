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
        [HttpPost("createReservation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateReservation(CreateReservationDto createReservation)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _reservationServices.CreateReservation(createReservation, id);

            return Ok(result);
        }

        [HttpGet("reservations")]
        [ProducesResponseType(typeof(List<GetReservationsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservations()
        {
            var result = await _reservationServices.GetReservations();

            return Ok(result);
        }

        [HttpGet("reservations/fill/data")]
        [ProducesResponseType(typeof(GetReservationFillDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReservationFillData(int entertainmentId)
        {
            var result = await _reservationServices.GetReservationFillData(entertainmentId);

            return Ok(result);
        }
    }
}
