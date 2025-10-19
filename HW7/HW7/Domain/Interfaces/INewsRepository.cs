using HW7.Domain.Entities;
namespace HW7.Domain.Interfaces
{
    public interface INewsRepository
    {
        Task<News> GetByIdAsync(Guid id);
        Task<IEnumerable<News>> GetAllAsync();
        Task AddAsync(News news);
        Task UpdateAsync(News news);
        Task DeleteAsync(Guid id);
    }
}
