using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using Token.Data;
using Token.Models;
using Token.Services;

namespace Token.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;

        public AuthController(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var user = _db.Users.FirstOrDefault(u => u.Username == req.Username);
            if (user == null || user.IsBlocked || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
                return Unauthorized(new { message = "Incorrect username, password or account locked!" });

            var token = _jwt.GenerateToken(user.Id, user.Username);
            return Ok(new { token, username = user.Username });
        }
    }
}
