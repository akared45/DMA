using CodeFirstClean.Application.DTOs;

namespace CodeFirstClean.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> AddAsync(BookDto dto);
        Task UpdateAsync(BookDto dto);
        Task DeleteAsync(int id);
    }
}
