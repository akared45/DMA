using CodeFirstClean.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly HospitalContext _context;
        public HospitalsController(HospitalContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hospital>>> GetAll()
        {
            var hospitals = await _context.Hospitals.Include(h => h.Doctors).ToListAsync();
            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hospital>> GetById(int id)
        {
            var hospital = await _context.Hospitals.Include(h => h.Doctors)
                .FirstOrDefaultAsync(h => h.HospitalId == id);

            if (hospital == null)
                return NotFound("Không tìm thấy bệnh viện.");

            return Ok(hospital);
        }

        [HttpPost]
        public async Task<ActionResult<Hospital>> Create(Hospital hospital)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Hospitals.Add(hospital);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = hospital.HospitalId }, hospital);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Hospital hospital)
        {
            if (id != hospital.HospitalId)
                return BadRequest("ID không khớp giữa URL và dữ liệu.");

            _context.Entry(hospital).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Hospitals.Any(e => e.HospitalId == id))
                    return NotFound("Không tìm thấy bệnh viện để cập nhật.");
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
                return NotFound("Không tìm thấy bệnh viện.");

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
