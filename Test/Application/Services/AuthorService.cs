using Test.Application.DTOs;
using Test.Application.Interfaces;
using Test.Domain.Entities;
using Test.Domain.Repository;

namespace Test.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        public AuthorService(IRepository<Author> repository)
        {
            _repository = repository;
        }
        public async Task<AuthorDTO> AddAsync(AuthorDTO dto)
        {
            var entity = new Author
            {
                Name = dto.Name,
                Biography = dto.Biography,
            };
            var result = await _repository.CreateAsync(entity);
            return new AuthorDTO
            {
                Name = result.Name,
                Biography = result.Biography,
            };
        }
        public async Task<AuthorDTO> GetByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;
            return new AuthorDTO
            {
                Id = author.Id,
                Name = author.Name,
                Biography = author.Biography,
            };
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteAsync(entity);
            }
        }
        public async Task<IEnumerable<AuthorDTO>> GetAllAsync()
        {
            var list_author = await _repository.GetAllAsync();
            return list_author.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = a.Name,
                Biography = a.Biography
            });
        }
        public async Task UpdateAsync(AuthorDTO dto)
        {
            var entity = new Author
            {
                Name = dto.Name,
                Biography = dto.Biography,
            };
            await _repository.UpdateAsync(entity);
        }
    }
}
