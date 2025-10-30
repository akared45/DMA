using Test.Application.DTOs;

namespace Test.Application.Interfaces
{
    public interface IAuthorService
    {
        Task<AuthorDTO> GetByIdAsync(int id);
        Task<IEnumerable<AuthorDTO>> GetAllAsync();
        Task<AuthorDTO> AddAsync(AuthorDTO dto);
        Task UpdateAsync(AuthorDTO dto);
        Task DeleteAsync(int id);
    }
}
