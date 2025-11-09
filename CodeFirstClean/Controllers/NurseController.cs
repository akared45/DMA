using CodeFirstClean.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstClean.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NursesController : ControllerBase
    {
        private readonly HospitalContext _context;

        public NursesController(HospitalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nurse>>> GetAll()
        {
            var nurses = await _context.Nurses
                .Include(x => x.Ward)
                .ToListAsync();

            return Ok(nurses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Nurse>> GetById(int id)
        {
            var nurse = await _context.Nurses
                .Include(x => x.Ward)
                .FirstOrDefaultAsync(x => x.NurseId == id);

            if (nurse == null)
                return NotFound("Không tìm thấy y tá.");

            return Ok(nurse);
        }

        [HttpPost]
        public async Task<ActionResult<Nurse>> Create(Nurse nurse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ward = await _context.Wards
                .FirstOrDefaultAsync(w => w.WardId == nurse.WardId);

            if (ward == null)
                return BadRequest("Mã khoa (WardId) không hợp lệ.");

            _context.Nurses.Add(nurse);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = nurse.NurseId }, nurse);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Nurse nurse)
        {
            if (id != nurse.NurseId)
                return BadRequest("ID không khớp.");

            var exists = await _context.Wards
                .AnyAsync(w => w.WardId == nurse.WardId);

            if (!exists)
                return BadRequest("Mã khoa không tồn tại.");

            _context.Entry(nurse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var nurseExists = await _context.Nurses.AnyAsync(x => x.NurseId == id);
                if (!nurseExists)
                    return NotFound("Không tìm thấy y tá để cập nhật.");

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse == null)
                return NotFound("Không tìm thấy y tá để xóa.");

            _context.Nurses.Remove(nurse);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
