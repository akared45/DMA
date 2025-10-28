using HW8.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static HW8.Application.DTOs.AuthDto;

namespace HW8.Presentation.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Incorrect username or password");
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<TokenResponse>> Refresh([FromBody] RefreshRequest request)
        {
            try
            {
                var result = await _authService.RefreshAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Refresh token invalid");
            }
        }
    }
}
