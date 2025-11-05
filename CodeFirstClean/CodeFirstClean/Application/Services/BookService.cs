using CodeFirstClean.Application.DTOs;
using CodeFirstClean.Application.Interfaces;
using CodeFirstClean.Domain.Entities;
using CodeFirstClean.Domain.Interfaces;

namespace CodeFirstClean.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            return books.Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Genre = b.Genre,
                PublicationYear = b.PublicationYear,
                AuthorId = b.AuthorId
            });
        }
        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return null;
            return new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                Genre = book.Genre,
                PublicationYear = book.PublicationYear,
                AuthorId = book.AuthorId
            };
        }
        public async Task<BookDto> AddAsync(BookDto dto)
        {
            var entity = new Book
            {
                Title = dto.Title,
                Genre = dto.Genre,
                PublicationYear = dto.PublicationYear,
                AuthorId = dto.AuthorId
            };


            var added = await _repository.AddAsync(entity);
            dto.BookId = added.BookId;
            return dto;
        }
        public async Task UpdateAsync(BookDto dto)
        {
            var entity = new Book
            {
                BookId = dto.BookId,
                Title = dto.Title,
                Genre = dto.Genre,
                PublicationYear = dto.PublicationYear,
                AuthorId = dto.AuthorId
            };
            await _repository.UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
