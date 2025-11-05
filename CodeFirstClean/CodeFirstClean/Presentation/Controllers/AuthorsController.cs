namespace CodeFirstClean.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _authorService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.AuthorId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AuthorDto dto)
        {
            if (id != dto.AuthorId) return BadRequest("ID mismatch");
            await _authorService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
