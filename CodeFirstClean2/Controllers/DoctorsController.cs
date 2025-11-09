using CodeFirstClean.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly HospitalContext _context;
        public DoctorsController(HospitalContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAll()
        {
            return await _context.Doctors.Include(d => d.Hospital).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetById(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Hospital)
                .FirstOrDefaultAsync(d => d.DoctorId == id);

            if (doctor == null)
                return NotFound("Không tìm thấy bác sĩ.");

            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<Doctor>> Create(Doctor doctor)
        {
            var exists = await _context.Hospitals.AnyAsync(h => h.HospitalId == doctor.HospitalId);
            if (!exists)
                return BadRequest("HospitalId không hợp lệ.");

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = doctor.DoctorId }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId)
                return BadRequest("ID không khớp giữa URL và dữ liệu.");

            _context.Entry(doctor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
