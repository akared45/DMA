using CodeFirstClean.Models;
using HospitalApi.Data;
using HospitalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardsController : ControllerBase
    {
        private readonly HospitalContext _context;

        public WardsController(HospitalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ward>>> GetAll()
        {
            var wards = await _context.Wards
                .Include(w => w.Nurses)
                .ToListAsync();

            return Ok(wards);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Ward>> GetById(int id)
        {
            var ward = await _context.Wards
                .Include(w => w.Nurses)
                .FirstOrDefaultAsync(w => w.WardId == id);

            if (ward == null)
                return NotFound("Không tìm thấy khoa.");

            return Ok(ward);
        }
        [HttpPost]
        public async Task<ActionResult<Ward>> Create(Ward ward)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Wards.Add(ward);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ward.WardId }, ward);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Ward ward)
        {
            if (id != ward.WardId)
                return BadRequest("ID không khớp.");

            _context.Entry(ward).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.Wards.AnyAsync(w => w.WardId == id);
                if (!exists)
                    return NotFound("Không tìm thấy khoa để cập nhật.");

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ward = await _context.Wards.FindAsync(id);
            if (ward == null)
                return NotFound("Không tìm thấy khoa để xóa.");

            _context.Wards.Remove(ward);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
