using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto.UserDto;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [Authorize]
        [HttpGet("userInfo")]
        [ProducesResponseType(typeof(GetUserDataDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserInformation()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _userServices.GetUserData(id);

            return Ok(result);
        }

        [Authorize]
        [HttpPatch("userInfo")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateUserInformation(UpdateUserInfo updateUserInfo)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _userServices.EditUserInfo(updateUserInfo, id);

            return Created(string.Empty, result);
        }

        [Authorize]
        [HttpDelete("user")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _userServices.DeleteUser(id);

            return Ok(result);
        }
    }
}
