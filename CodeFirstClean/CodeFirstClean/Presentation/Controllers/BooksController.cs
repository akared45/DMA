namespace CodeFirstClean.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _bookService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BookId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookDto dto)
        {
            if (id != dto.BookId) return BadRequest("ID mismatch");
            await _bookService.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return NoContent();
        }
    }
}
