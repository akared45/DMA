using CodeFirstClean.Application.DTOs;
using CodeFirstClean.Application.Interfaces;
using CodeFirstClean.Domain.Entities;
using CodeFirstClean.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CodeFirstClean.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepository<Author> _repository;
        public AuthorService(IRepository<Author> repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _repository.GetAllAsync();
            return authors.Select(a => new AuthorDto
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                Biography = a.Biography
            });
        }
        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;
            return new AuthorDto
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Biography = author.Biography
            };
        }
        public async Task<AuthorDto> AddAsync(AuthorDto dto)
        {
            var entity = new Author { Name = dto.Name, Biography = dto.Biography };
            var added = await _repository.AddAsync(entity);
            dto.AuthorId = added.AuthorId;
            return dto;
        }
        public async Task UpdateAsync(AuthorDto dto)
        {
            var entity = new Author { AuthorId = dto.AuthorId, Name = dto.Name, Biography = dto.Biography };
            await _repository.UpdateAsync(entity);
        }
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
