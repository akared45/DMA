using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBFirst.Models;

namespace DBFirst.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LecturerController : ControllerBase
    {
        private readonly UniversitydbContext _context;

        public LecturerController(UniversitydbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lecturer>>> GetLecturers()
        {
            return await _context.Lecturers
                                 .Include(l => l.Department)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lecturer>> GetLecturer(int id)
        {
            var lecturer = await _context.Lecturers
                                         .Include(l => l.Department)
                                         .FirstOrDefaultAsync(l => l.LecturerId == id);

            if (lecturer == null)
                return NotFound();

            return lecturer;
        }

        [HttpPost]
        public async Task<ActionResult<Lecturer>> CreateLecturer(Lecturer lecturer)
        {
            _context.Lecturers.Add(lecturer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLecturer), new { id = lecturer.LecturerId }, lecturer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLecturer(int id, Lecturer lecturer)
        {
            if (id != lecturer.LecturerId)
                return BadRequest();

            _context.Entry(lecturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecturer(int id)
        {
            var lecturer = await _context.Lecturers.FindAsync(id);
            if (lecturer == null)
                return NotFound();

            _context.Lecturers.Remove(lecturer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LecturerExists(int id)
        {
            return _context.Lecturers.Any(e => e.LecturerId == id);
        }
    }
}
