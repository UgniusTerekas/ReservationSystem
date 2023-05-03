using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.Entertainment;
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

        [Authorize]
        [HttpGet("userReservations")]
        [ProducesResponseType(typeof(List<GetReservationInfoForCustomer>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserReservations()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _reservationServices.GetReservationsForUser(id);

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

        [Authorize]
        [HttpPatch("createUserReservation")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUserReservation(CreateUserReservationDto createUserReservationDto)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _reservationServices.CreateUserReservation(createUserReservationDto, id);

            return Created(string.Empty, result);
        }

        [HttpGet("entertainmentsReservations")]
        [ProducesResponseType(typeof(List<EntertainmentReservationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEntertainmentReservations(
            int entertainmentId,
            string date)
        {
            var result = await _reservationServices.GetEntertainmentReservations(entertainmentId, date);

            return Ok(result);
        }

        [Authorize]
        [HttpGet("adminDashboard/reservations")]
        [ProducesResponseType(typeof(List<GetAdminReservationsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdminDashboardReservations()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _reservationServices.GetAdminReservations(id);

            return Ok(result);
        }

        [HttpDelete("userReservations")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserReservation(int reservationId)
        {
            var result = await _reservationServices.DeleteUserReservation(reservationId);

            return Ok(result);
        }
    }
}
