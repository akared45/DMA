using CodeFirstClean.Application.DTOs;

namespace CodeFirstClean.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task<AuthorDto> AddAsync(AuthorDto dto);
        Task UpdateAsync(AuthorDto dto);
        Task DeleteAsync(int id);
    }
}
