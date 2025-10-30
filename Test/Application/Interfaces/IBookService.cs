using Test.Application.DTOs;

namespace Test.Application.Interfaces
{
    public interface IBookService
    {
        Task<BookDTO> GetByIdAsync(int id);
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task<BookDTO> AddAsync(BookDTO dto);
        Task UpdateAsync(BookDTO dto);
        Task DeleteAsync(int id);
    }
}
