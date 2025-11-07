using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Token.Data;

namespace Token.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AdminController: ControllerBase
    {
        private readonly AppDbContext _db;

        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _db.Users.Select(u => new
            {
                u.Id,
                u.Username,
                u.IsBlocked
            }).ToList();

            return Ok(users);
        }

        [HttpPost("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.IsBlocked = true;
            await _db.SaveChangesAsync();

            return Ok(new { message = $"Đã khóa user: {user.Username}" });
        }
    }
}
