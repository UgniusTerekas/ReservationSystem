using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Contracts.Auth;
using ServiceLayer.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Register (RegisterRequest registerContract)
        {
            var result = await _authServices.Register(registerContract);

            if (result == null)
            {
                return Ok(false);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login (LoginRequest loginRequest)
        {
            var result = await _authServices.Login(loginRequest);

            if (result == null)
            {
                return Ok("");
            }

            return Ok(result);
        }
    }
}
