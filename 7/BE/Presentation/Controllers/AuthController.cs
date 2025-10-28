using HW7.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HW7.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        public AuthController(LoginUseCase loginUseCase)
        {
            _loginUseCase = loginUseCase;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var token = _loginUseCase.Execute(request.Username, request.Password);
                return Ok(new { AccessToken = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid username or password");
            }
        }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
